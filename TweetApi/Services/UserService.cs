using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TweetApi.Context;
using TweetApi.Models;

namespace TweetApi.Security
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;

        public UserService(IMapper mapper, IConfiguration configuration, DataContext dataContext)
        {
            _mapper = mapper;
            _configuration = configuration;
            _dataContext = dataContext;
        }

        /// <summary>
        /// Handles forget password
        /// </summary>
        /// <param name="forgetPasswordDto"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<string>> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        {
            var response = new ServiceResponse<string>();
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == forgetPasswordDto.UserName.ToLower());

            if (user != null)
            {
                CreatePasswordHash(forgetPasswordDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                await _dataContext.SaveChangesAsync();

                response.Data = CreateToken(user);
                response.Message = "Password changed successfully";
                return response;
            }

            response.Success = false;
            response.Message = "User not found";
            return response;
        }

        /// <summary>
        /// Fetch All users
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<List<UserDetailsDto>>> GetAllUsers()
        {
            var response = new ServiceResponse<List<UserDetailsDto>>();
            var user = await _dataContext.Users.ToListAsync();
            response.Success = true;
            response.Data = _mapper.Map<List<UserDetailsDto>>(user);
            return response;
        }

        /// <summary>
        /// Fetch User by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<UserDetailsDto>> GetUserByUserName(string userName)
        {
            var response = new ServiceResponse<UserDetailsDto>();
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
            response.Success = true;
            response.Data = _mapper.Map<UserDetailsDto>(user);
            return response;
        }

        /// <summary>
        /// Handles Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<string>> Login(string userName, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(userName.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            else
            {
                response.Data = "Bearer " + CreateToken(user);
            }

            return response;
        }

        /// <summary>
        /// Handles User Registration
        /// </summary>
        /// <param name="userDto">User Details</param>
        /// <param name="password">Password</param>
        /// <returns>User Details</returns>
        public async Task<ServiceResponse<UserDto>> Register(UserDto userDto, string password)
        {
            var response = new ServiceResponse<UserDto>();
            var user = _mapper.Map<User>(userDto);
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            if (await UserNameExists(user.UserName))
            {
                response.Success = false;
                response.Message = "Username already exists";
                return response;
            }
            if (await EmailExists(user.Email))
            {
                response.Success = false;
                response.Message = "Email already exists";
                return response;
            }

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            response.Data = _mapper.Map<UserDto>(user);
            response.Message = "User registerd succesfully";
            return response;
        }

        /// <summary>
        /// Validate if email is exists or not.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> EmailExists(string email)
        {
            if (await _dataContext.Users.AnyAsync(u => u.Email == email))
                return true;
            return false;
        }

        /// <summary>
        /// Validate if user name is exists or not.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> UserNameExists(string userName)
        {
            if (await _dataContext.Users.AnyAsync(u => u.UserName == userName))
                return true;
            return false;
        }

        /// <summary>
        /// Create Password Hash
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="passwordHash">out Password Hash</param>
        /// <param name="passwordSalt">out Password Salt</param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);

        }

        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

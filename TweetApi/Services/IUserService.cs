using TweetApi.Models;

namespace TweetApi.Security
{
    public interface IUserService
    {
        /// <summary>
        /// Handles User Registration
        /// </summary>
        /// <param name="userDto">User Details</param>
        /// <param name="password">Password</param>
        /// <returns>User Details</returns>
        Task<ServiceResponse<UserDto>> Register(UserDto userDto, string password);

        /// <summary>
        /// Handles Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ServiceResponse<string>> Login(string userName, string password);

        /// <summary>
        /// Validate if email is exists or not.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> EmailExists(string email);

        /// <summary>
        /// Validate if user name is exists or not.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> UserNameExists(string userName);

        /// <summary>
        /// Handles forget password
        /// </summary>
        /// <param name="forgetPasswordDto"></param>
        /// <returns></returns>
        Task<ServiceResponse<string>> ForgetPassword(ForgetPasswordDto forgetPasswordDto);

        /// <summary>
        /// Fetch All users
        /// </summary>
        /// <returns></returns>
        Task<ServiceResponse<List<UserDetailsDto>>> GetAllUsers();

        /// <summary>
        /// Fetch User by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<ServiceResponse<UserDetailsDto>> GetUserByUserName(string userName);
    }
}

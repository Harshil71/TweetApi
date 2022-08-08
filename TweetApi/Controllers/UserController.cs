using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TweetApi.Models;
using TweetApi.Security;

namespace TweetApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/{version:apiVersion}/tweets")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Register(userDto, userDto.Password);
                if (!response.Success)
                    return BadRequest(response);
                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var response = await _userService.Login(userName, password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("forgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto forgetPasswordDto)
        {
            var response = await _userService.ForgetPassword(forgetPasswordDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("users/all")]
        public async Task<IActionResult> Get()
        {
            var response = await _userService.GetAllUsers();
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("user/search/{username}")]
        public async Task<IActionResult> Get(string username)
        {
            var response = await _userService.GetUserByUserName(username);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

    }
}

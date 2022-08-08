using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TweetApi.Models;
using TweetApi.Services;

namespace TweetApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/{version:apiVersion}/tweets")]
    [Authorize]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;

        public TweetController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        [HttpPost("{userName}/add")]
        public async Task<IActionResult> Add(string userName, TweetDto tweetDto)
        {
            var response = await _tweetService.Add(tweetDto, userName);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{userName}/udpate/{id}")]
        public async Task<IActionResult> Update(string userName, int id, TweetDto tweetDto)
        {
            var response = await _tweetService.Update(id, tweetDto, userName);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{userName}/delete/{id}")]
        public async Task<IActionResult> Delete(string userName, int id)
        {
            var response = await _tweetService.Delete(id, userName);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var response = await _tweetService.GetAllTweets();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{userName}/all")]
        public async Task<IActionResult> Get(string userName)
        {
            var response = await _tweetService.GetAllTweetsByUserName(userName);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

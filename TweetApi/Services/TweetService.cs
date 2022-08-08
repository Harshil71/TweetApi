using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TweetApi.Context;
using TweetApi.Models;

namespace TweetApi.Services
{
    public class TweetService : ITweetService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public TweetService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<TweetDetailDto>> Add(TweetDto tweetDto, string userName)
        {
            var response = new ServiceResponse<TweetDetailDto>();
            User user = new();

            if (string.IsNullOrEmpty(userName))
            {
                response.Success = false;
                response.Message = "Username is required.";
            }
            else
            {
                user = await _dataContext.Users.FirstOrDefaultAsync(predicate: u => u.UserName.ToLower() == userName.ToLower());
                if (user == null) ;
                response.Success = false;
                response.Message = "User not found.";
            }

            var tweet = _mapper.Map<Tweet>(tweetDto);
            await _dataContext.AddAsync(tweet);
            tweet.CreatedByUser = user;

            await _dataContext.SaveChangesAsync();

            response.Data = _mapper.Map<TweetDetailDto>(tweet);
            response.Message = "Tweet created successfully";
            return response;
        }

        public async Task<ServiceResponse<string>> Delete(int tweetId, string userName)
        {
            var response = new ServiceResponse<string>();
            var tweet = await _dataContext.Tweets.FirstOrDefaultAsync(t => t.Id == tweetId);

            if (tweet == null)
            {
                response.Success = false;
                response.Message = "Tweet not found";
                return response;
            }

            _dataContext.Tweets.Remove(tweet);
            await _dataContext.SaveChangesAsync();
            response.Data = "Tweet deleted successfully";
            return response;
        }

        public async Task<ServiceResponse<List<TweetDetailDto>>> GetAllTweets()
        {
            var response = new ServiceResponse<List<TweetDetailDto>>();
            var tweet = await _dataContext.Tweets.ToListAsync();
            response.Success = true;
            response.Data = _mapper.Map<List<TweetDetailDto>>(tweet);
            return response;
        }

        public async Task<ServiceResponse<List<TweetDetailDto>>> GetAllTweetsByUserName(string userName)
        {
            var response = new ServiceResponse<List<TweetDetailDto>>();

            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());

            var tweet = await _dataContext.Tweets.FirstOrDefaultAsync(predicate: t => t.CreatedByUser.Id == user.Id);

            response.Success = true;
            response.Data = _mapper.Map<List<TweetDetailDto>>(tweet);
            return response;
        }

        public Task<ServiceResponse<string>> Reply(TweetDto tweetDto, int tweetId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<TweetDetailDto>> Update(int tweetId, TweetDto tweetDto, string userName)
        {
            var response = new ServiceResponse<TweetDetailDto>();
            var tweet = await _dataContext.Tweets.FirstOrDefaultAsync(t => t.Id == tweetId);

            tweet.TweetMessage = tweetDto.TweetMessage;
            tweet.UpdatedDate = DateTime.Now;

            if (tweet == null)
            {
                response.Success = false;
                response.Message = "No Tweet Found";
                return response;
            }

            await _dataContext.SaveChangesAsync();
            response.Data = _mapper.Map<TweetDetailDto>(tweet);
            response.Message = "Tweet updated successfully";
            return response;
        }
    }
}

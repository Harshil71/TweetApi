using TweetApi.Models;

namespace TweetApi.Services
{
    public interface ITweetService
    {
        Task<ServiceResponse<TweetDetailDto>> Add(TweetDto tweetDto, string userName);

        Task<ServiceResponse<string>> Reply(TweetDto tweetDto, int tweetId);

        Task<ServiceResponse<TweetDetailDto>> Update(int tweetId, TweetDto tweetDto, string userName);

        Task<ServiceResponse<string>> Delete(int tweetId, string userName);

        Task<ServiceResponse<List<TweetDetailDto>>> GetAllTweets();

        Task<ServiceResponse<List<TweetDetailDto>>> GetAllTweetsByUserName(string userName);
    }
}

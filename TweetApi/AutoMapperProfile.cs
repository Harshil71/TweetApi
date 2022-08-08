using AutoMapper;
using TweetApi.Models;

namespace TweetApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserDetailsDto, User>();
            CreateMap<User, UserDetailsDto>();
            CreateMap<Tweet, TweetDto>();
            CreateMap<TweetDto, Tweet>();
            CreateMap<Tweet, TweetDetailDto>();
            CreateMap<TweetDetailDto, Tweet>();
        }
    }
}

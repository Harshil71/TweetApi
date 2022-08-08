using Microsoft.EntityFrameworkCore;
using TweetApi.Models;

namespace TweetApi.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Tweet> Tweets { get; set; }
    }
}

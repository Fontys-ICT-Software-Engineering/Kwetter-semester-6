using Microsoft.EntityFrameworkCore;
using Kweet.Models;
using KweetService.Models;

namespace Kweet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<KweetModel> Kweets { get; set; }

        public DbSet<Like> Likes { get; set; }  

        public DbSet<ReactionKweet> Reactions { get; set; } 
    }
}

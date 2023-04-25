using Microsoft.EntityFrameworkCore;
using Kweet.Models;
using KweetService.Models;

namespace Kweet.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<KweetModel> Kweets { get; set; }

        public virtual DbSet<Like> Likes { get; set; }  

        public DbSet<ReactionKweet> Reactions { get; set; } 
    }
}

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid id1 = Guid.NewGuid();

            modelBuilder.Entity<KweetModel>().HasData(new KweetModel { Id = id1, Date = DateTime.Now, IsEdited = false, Message = "testMessage 1", User = "User1" });
            modelBuilder.Entity<KweetModel>().HasData(new KweetModel { Id = Guid.NewGuid(), Date = DateTime.Now, IsEdited = true, Message = "testMessage 2", User = "User1" });
            modelBuilder.Entity<KweetModel>().HasData(new KweetModel { Id = Guid.NewGuid(), Date = DateTime.Now, IsEdited = false, Message = "testMessage 3", User = "User1" });
            modelBuilder.Entity<KweetModel>().HasData(new KweetModel { Id = Guid.NewGuid(), Date = DateTime.Now, IsEdited = false, Message = "testMessage 4", User = "User1" });
            modelBuilder.Entity<KweetModel>().HasData(new KweetModel { Id = Guid.NewGuid(), Date = DateTime.Now, IsEdited = true, Message = "testMessage 5", User = "User2" });
            modelBuilder.Entity<KweetModel>().HasData(new KweetModel { Id = Guid.NewGuid(), Date = DateTime.Now, IsEdited = false, Message = "testMessage 6", User = "User2" });

            modelBuilder.Entity<Like>().HasData(new Like { Id = 1, KweetID = id1.ToString(), UserID = "User2" });
            modelBuilder.Entity<Like>().HasData(new Like { Id = 2, KweetID = id1.ToString(), UserID = "User1" });
            modelBuilder.Entity<Like>().HasData(new Like { Id = 3, KweetID = id1.ToString(), UserID = "User1" });

        }

        public virtual DbSet<KweetModel> Kweets { get; set; }

        public virtual DbSet<Like> Likes { get; set; }  

        public DbSet<ReactionKweet> Reactions { get; set; } 
    }
}

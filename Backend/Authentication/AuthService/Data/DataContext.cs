using AuthService.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Collections.Generic;

namespace AuthService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid id1 = Guid.NewGuid();

            string password = BCrypt.Net.BCrypt.HashPassword("string");

            modelBuilder.Entity<User>().HasData(new User {Id = Guid.NewGuid(), DateEnrolled = DateTime.Now, Email = "string", Password = password, Role = UserRole.NORMAL });
        }

        public DbSet<User> users { get; set; }
    }
}

using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuthService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
    }
}

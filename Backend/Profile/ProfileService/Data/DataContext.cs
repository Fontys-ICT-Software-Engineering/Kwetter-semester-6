using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models;


namespace ProfileService.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Profile> Profiles { get; set; }

    }
}
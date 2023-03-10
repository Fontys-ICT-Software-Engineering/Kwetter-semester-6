using Microsoft.EntityFrameworkCore;
using Kweet.Models;



namespace Kweet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<KweetModel> Kweets { get; set; }
    }
}

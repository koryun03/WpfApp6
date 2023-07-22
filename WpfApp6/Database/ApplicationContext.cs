using Microsoft.EntityFrameworkCore;
using WpfApp6.Models;

namespace WpfApp6.Database
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
      
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            optionsbuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=db112;trusted_connection=true;");
        }
       
    }
}

using Microsoft.EntityFrameworkCore;

namespace AngularTutorial_API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bank> Banks { get; set; }
    }
}

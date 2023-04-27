using HealthService.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) :base(opt)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
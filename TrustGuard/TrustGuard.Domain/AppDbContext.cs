using Microsoft.EntityFrameworkCore;
using TrustGuard.Domain.Models;

namespace TrustGuard.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Casco> Cascos { get; set; }
        public DbSet<HealthInsurance> HealthInsurances { get; set; }
        public DbSet<Offer> Offers { get; set; }




    }

}

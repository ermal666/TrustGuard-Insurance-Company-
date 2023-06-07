using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrustGuard.Domain.Models;

namespace TrustGuard.Domain
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost; Database=TrustGuard; TrustServerCertificate=true");
            }
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<CascoInsurance> Cascos { get; set; }
        public DbSet<HealthInsurance> HealthInsurances { get; set; }
        public DbSet<TPLInsurance> TPLInsurances { get; set; }
        public DbSet<Offer> Offers { get; set; }


    }

}

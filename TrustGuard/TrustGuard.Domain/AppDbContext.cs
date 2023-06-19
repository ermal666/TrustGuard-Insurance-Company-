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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseSqlServer("Data Source=localhost;Database=TrustGuard;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true")
        //        .EnableSensitiveDataLogging();
        //}



        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<CascoInsurance> Cascos { get; set; }
        public DbSet<HealthInsurance> HealthInsurances { get; set; }
        public DbSet<TPLInsurance> TPLInsurances { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AccidentInsurance> AccidentInsurances { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }

}

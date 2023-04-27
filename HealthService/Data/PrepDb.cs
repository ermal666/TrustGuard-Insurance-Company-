using HealthService.Models;
 

namespace HealthService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }
    } 

    private static void SeedData(AppDbContext context)
    {
        if (!context.Users.Any())
        {
            Console.WriteLine("Seeding Data");

            context.Users.AddRange(
                new User() {PersonalId = "12123123421", FullName = "Aurela Gashi",BirthDate = DateTime.Now ,Address = "Address",Email = "ag48111@ubt-uni.net",PhoneNumber = "045844508"}
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("We already have data");
        }
    }
    
}
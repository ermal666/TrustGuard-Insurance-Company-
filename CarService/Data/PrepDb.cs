using CarService.Models;

namespace CarService.Data;

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
                new User() {PersonalId = "12123123421", FullName = "Ermal Komoni",BirthDate = DateTime.Now ,Address = "Address",Email = "Ermalkk14@gmail.com",PhoneNumber = "049704880"}
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("We already have data");
        }
    }
    
}
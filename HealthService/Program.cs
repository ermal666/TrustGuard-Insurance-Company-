using HealthService.Data;
using HealthService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase(databaseName: "myInMemoryDb"));

//CONFIGURING DEPENDENCY INJECTION
builder.Services.AddScoped<IUserRepo, UserRepo>();

var app = builder.Build();

PrepDb.PrepPopulation(app);

//app.MapGet("/", () => "Hello World!");

app.Run();
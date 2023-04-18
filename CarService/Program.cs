using CarService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase(databaseName: "myInMemoryDb"));

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run();

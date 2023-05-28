using Microsoft.EntityFrameworkCore;
using TrustGuard.Domain;
using TrustGuard.Persistence.IRepositories;
using TrustGuard.Persistence.Repositories;
using TrustGuard.Service.IServices;
using TrustGuard.Service.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeafaultConnection")));

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
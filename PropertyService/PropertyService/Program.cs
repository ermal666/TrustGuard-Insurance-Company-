using Microsoft.EntityFrameworkCore;
using PropertyService.Data;
using PropertyService.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<IAppDbContext, AppDbContext>(options =>
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

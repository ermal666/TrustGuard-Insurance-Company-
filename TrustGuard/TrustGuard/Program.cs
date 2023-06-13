using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using TrustGuard.Domain;
using TrustGuard.Domain.Configurations;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;
using TrustGuard.Persistence.Repositories;
using TrustGuard.Service.IServices;
using TrustGuard.Service.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DeafaultConnection")));
services.AddDbContext<AppDbContext>();

// adding Stripe configuration
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

// adding jwt configuration
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);

var tokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false, // update when you add refresh tokens
    ValidateAudience = false, // update when you add refresh tokens
    RequireExpirationTime = false, // update when you add refresh tokens
    ValidateLifetime = true
};

// adding authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwt =>
    {
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = tokenValidationParameters;
    });
builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = false)
    .AddEntityFrameworkStores<AppDbContext>();

// dependency injection
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<ICascoRepository, CascoRepository>();
services.AddScoped<ITPLRepository, TPLRepository>();
services.AddScoped<IHealthRepository, HealthRepository>();
services.AddScoped<IAccidentRepository, AccidentRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

// stripe configuration
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
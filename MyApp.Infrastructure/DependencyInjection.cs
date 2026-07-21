using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyApp.Application.Common.JWT;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Services;
using System.Text;
namespace MyApp.Infrastructure
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=.;Database=MyAppCA;Trusted_Connection=True;TrustServerCertificate=True;"));
            services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer
                            (configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
            services.Configure<JwtSettings>(
                    configuration.GetSection("JwtSettings"));

            services.AddScoped<IJwtService, JwtService>();


            //AddAuthentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
.AddJwtBearer(options =>
{
    var jwtSettings = configuration
        .GetSection("JwtSettings")
        .Get<JwtSettings>();

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings!.Issuer,
        ValidAudience = jwtSettings.Audience,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

            return services;
        }
    }
}

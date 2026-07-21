using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using MyApp.API.Middlewares;
using MyApp.Application.Configurations;

using Serilog;
namespace MyApp.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .WriteTo.Console()
                        .WriteTo.File(
                    "Logs/log-.txt",
                    rollingInterval: RollingInterval.Day)
                      .CreateLogger();
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Configure<StudentSettings>(
            builder.Configuration.GetSection("StudentSettings"));
            builder.Host.UseSerilog();

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MyApp API",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });
            builder.Services.AddApiDI(builder.Configuration);
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            var app = builder.Build();
            //swagger

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApp API V1");
            });
            // Configure the HTTP request pipeline.

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                await MyApp.Infrastructure.Identity.IdentitySeeder
                    .SeedRolesAsync(roleManager);
            }
            app.Run();
        }
    }
}

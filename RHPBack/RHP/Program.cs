using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RHP.Data;
using RHP.Entities.Models;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        //Database connection
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        var services = builder.Services;

        //Add all services to the DI container
        var assembly = Assembly.GetExecutingAssembly();


        services.AddAutoMapper(typeof(Program).Assembly);
        //services.AddScoped<AuthenticationService>();
        foreach (var type in assembly.GetTypes()
            .Where(t => t.Namespace == "RHP.API.Services")
            .Where(t => t.IsClass && !t.IsAbstract))
        {
            services.AddScoped(type);
        }

        // Add all repositories to the DI container
        //services.AddScoped<UserRepository>();

        foreach (var type in assembly.GetTypes()
                       .Where(t => t.Namespace == "RHP.API.Repositories")
                                  .Where(t => t.IsClass && !t.IsAbstract))
        {
            services.AddScoped(type);
        }
        // Add AuthenticationService to the DI container
        builder.Services.AddScoped<AuthenticationService>();

        //JWT Authentication
        using var hmac = new HMACSHA256();
        var key = hmac.Key;

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //CORS Handling
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                               builder =>
                               {
                                   builder.WithOrigins("http://localhost:4200")
                                       .AllowAnyHeader()
                                       .AllowAnyMethod();
                               });
        });

        var app = builder.Build();

        
        //Database migration
        //Migrate and seed the database during startup

        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;

            try
            {
                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.Migrate();

                if (!context.User.Any())
                {
                    var adminUser = new User
                    {
                        Email = "admin@admin.com",
                        Role = UserRole.Admin,
                        Password = BCrypt.Net.BCrypt.HashPassword("admin")
                    };

                    context.User.Add(adminUser);
                    await context.SaveChangesAsync();

                    Player adminPlayer = new Player
                    {
                        Name = "Admin",
                        User = adminUser
                    };

                    context.Player.Add(adminPlayer);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database");
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();


        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RHP.API.Hubs;
using RHP.API.Services;
using RHP.Data;
using System.Reflection;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureDatabase(builder);
        ConfigureServices(builder);
        ConfigureAuthentication(builder);
        ConfigureSignalR(builder);
        ConfigureCors(builder);

        var app = builder.Build();

        MigrateDatabase(app);

        ConfigurePipeline(app);
        MapHubs(app);

        app.Run();
    }

    private static void MapHubs(WebApplication app)
    {
        app.MapHub<ContactHub>("hub/contacts");
    }

    private static void ConfigureSignalR(WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR();

    }

    private static void ConfigureDatabase(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddScoped<AuthenticationService>();
        services.AddScoped<UserService>();
        services.AddScoped<PlayerService>();
        services.AddScoped<HallService>();

        foreach (var type in assembly.GetTypes()
            .Where(t => t.Namespace == "RHP.API.Repositories")
            .Where(t => t.IsClass && !t.IsAbstract))
        {
            services.AddScoped(type);
        }

        foreach (var type in assembly.GetTypes()
            .Where(t => t.Namespace == "RHP.Entities.Models.Mappers")
            .Where(t => t.IsClass))
        {
            services.AddAutoMapper(type.Assembly);
        }

        services.AddScoped<AuthenticationService>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
    }

    private static void ConfigureAuthentication(WebApplicationBuilder builder)
    {
        string keyString = AuthenticationService.GenerateKey();
        var key = Convert.FromBase64String(keyString);

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
        builder.Services.AddAuthorization();
    }

    private static void ConfigureCors(WebApplicationBuilder builder)
    {
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
    }

    private static void MigrateDatabase(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;

            try
            {
                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.Migrate();

                if (!context.Users.Any())
                {
                    DbInitialiser.SeedAdminUser(context);
                    DbInitialiser.SeedDb(context);
                }
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database");
            }
        }
    }

    private static void ConfigurePipeline(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

public class AuthenticationService
{
    private readonly IConfiguration _configuration;
    private static readonly int KeySize = 32;

    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void AuthentificationService(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var key = GenerateKey();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private static byte[] GenerateKey()
    {
        using var randomNumberGenerator = new RNGCryptoServiceProvider();
        var randomNumber = new byte[KeySize];
        randomNumberGenerator.GetBytes(randomNumber);
        return randomNumber;
    }
}

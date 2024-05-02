using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using RHP.API.Repositories;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

public class AuthenticationService: IAuthenticationService
{
    private readonly UserRepository _userRepository;
    private readonly PlayerRepository _playerRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private static readonly byte[] key = Convert.FromBase64String(GenerateKey());

    public AuthenticationService(UserRepository userRepository, IHttpContextAccessor httpContextAccessor, PlayerRepository playerRepository)
    {
        _userRepository = userRepository;

        if (_userRepository == null)
        {
            throw new ArgumentNullException(nameof(userRepository));
        }

        _playerRepository = playerRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public string Login(UserLoginDTO dto)
    {
        int userId = CheckCredentials(dto);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userId.ToString()),
            new Claim(ClaimTypes.Email, dto.Email)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Bearer");

        //Generate token
        var tokenHandler = new JwtSecurityTokenHandler();

        //Generate Key
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }

    public Player getLoggedPlayer()
    {   

        var httpContext = _httpContextAccessor.HttpContext;


        //Manually parsing the JWT token while investigating the issue with the Authorization header.
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
        var userIdClaim = token.Claims.First(claim => claim.Type == "unique_name");

        if (!string.IsNullOrEmpty(userIdClaim.Value))
        {
            return _playerRepository.GetPlayerByUserId(int.Parse(userIdClaim.Value));

        }else
        {
            throw new Exception("User not authenticated");
        }


        //var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
        //if(claimsIdentity == null)
        //{
        //    throw new Exception("User not authenticated");
        //}

        //string userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
        //if (userId == null)
        //{
        //    throw new Exception("User not authenticated");
        //}


    }

    private int CheckCredentials(UserLoginDTO dto)
    {
        User user = _userRepository.GetUserByEmail(dto.Email) ?? throw new Exception("Invalid username");
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            throw new Exception("Invalid password");
        }
        return user.Id;
    }

    public static string GenerateKey()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var key = new byte[32];
            rng.GetBytes(key);
            return Convert.ToBase64String(key);
        }
    }
}


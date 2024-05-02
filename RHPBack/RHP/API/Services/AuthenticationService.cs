using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.IdentityModel.Tokens;
using RHP.API.Repositories;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthenticationService: IAuthenticationService
{
    private readonly UserRepository _userRepository;
    private readonly PlayerRepository _playerRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _config;

    public AuthenticationService(UserRepository userRepository, IConfiguration config, IHttpContextAccessor httpContextAccessor, PlayerRepository playerRepository)
    {
        _userRepository = userRepository;

        if (_userRepository == null)
        {
            throw new ArgumentNullException(nameof(userRepository));
        }

        _playerRepository = playerRepository;
        _config = config;
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

        //Generate token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }

    public Player getLoggedPlayer()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
        if(claimsIdentity == null)
        {
            throw new Exception("User not authenticated");
        }

        string userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
        if (userId == null)
        {
            throw new Exception("User not authenticated");
        }

        return _playerRepository.GetPlayerByUserId(userId);

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
}


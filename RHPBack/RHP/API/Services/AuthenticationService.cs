using Microsoft.IdentityModel.Tokens;
using RHP.API.Repositories;
using RHP.API.Services;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

public class AuthenticationService
{
    private readonly UserRepository _userRepository;
    private readonly UserService _userService;
    private readonly PlayerService _playerService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private static readonly byte[] key = Convert.FromBase64String(GenerateKey());

    public AuthenticationService(UserRepository userRepository, IHttpContextAccessor httpContextAccessor, PlayerService playerService, UserService userService)
    {
        _userRepository = userRepository;

        if (_userRepository == null)
        {
            throw new ArgumentNullException(nameof(userRepository));
        }

        _userService = userService;
        _playerService = playerService;
        _httpContextAccessor = httpContextAccessor;
    }

    public string Login(UserLoginDTO dto)
    {
        User user = CheckCredentials(dto);

        //Update user Status and contacts on login
        user.lastLogin = DateTime.Now;
        user.loggedIn = true;

        _userService.UpdateUser(user);

        //Set Claims for JWT
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
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

    public void Logout()
    {
        Player player = GetLoggedPlayer();
        User user = player.User;

        user.loggedIn = false;
        _userService.UpdateUser(user);
    }

    public string GetLoggedUserId()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        // Manually parsing the JWT token while investigating the issue with the Authorization header.
        var authHeader = httpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            throw new Exception("Authorization header is missing or incorrectly formatted");
        }

        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(authHeader.Replace("Bearer ", ""));
        var userIdClaim = token.Claims.First(claim => claim.Type == "unique_name");


        if (!string.IsNullOrEmpty(userIdClaim.Value))
        {
            return userIdClaim.Value;
        }
        else
        {
            throw new Exception("User not authenticated");
        }
    }

    public User GetLoggedUser()
    {
        string userIdClaim = GetLoggedUserId();
        User user = _userService.GetUserById(userIdClaim).GetAwaiter().GetResult();
        return user;
    }

    public Player GetLoggedPlayer()
    {
        string userIdClaim = GetLoggedUserId();
        Player player = _playerService.GetPlayerByUserId(userIdClaim).GetAwaiter().GetResult();
        
        if(player is null)
        {
            throw new Exception("Player not found");
        }

        return player;
    }

    private User CheckCredentials(UserLoginDTO dto)
    {
        User user = _userRepository.GetUserByEmail(dto.Email) ?? throw new Exception("Invalid username");
       
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            throw new Exception("Invalid Password");
        }
        return user;
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


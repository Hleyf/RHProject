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
    private static readonly int KeySize = 32;

    public AuthenticationService(PlayerService playerService, UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public string Login(UserLoginDTO dto)
    {
        int userId = CheckCredentials(dto);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = GenerateKey();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("Id", userId.ToString()) }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public int CheckCredentials(UserLoginDTO dto)
    {
        User user = _userRepository.GetByUsername(dto.Email) ?? throw new Exception("Invalid username");
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            throw new Exception("Invalid password");
        }
        return user.Id;
    }

    private static byte[] GenerateKey()
    {
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        var randomNumber = new byte[KeySize];
        randomNumberGenerator.GetBytes(randomNumber);
        return randomNumber;
    }
}


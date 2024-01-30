using Microsoft.AspNetCore.Mvc;
using RHP.Entities.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDTO dto)
    {
        var token = _authService.Login(dto);

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }

        return Ok(new { Token = token });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(new { Message = "Logged out" });
    }


    [HttpGet("refresh-token")]
    public IActionResult RefreshToken()
    {
        var newToken = _authService.RefreshToken();

        return Ok(new { Token = newToken });
    }
}

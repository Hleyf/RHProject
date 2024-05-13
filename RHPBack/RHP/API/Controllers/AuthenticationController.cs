using Microsoft.AspNetCore.Mvc;
using RHP.Entities.Models.DTOs;

[Route("api/Auth")]
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
        try
        {
            var token = _authService.Login(dto);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(new { Message = "Logged out" });
    }

}

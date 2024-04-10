using Microsoft.AspNetCore.Mvc;
using RHP.API.Services;

namespace RHP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }



        [HttpPost]
        public IActionResult CreatePlayerUser([FromBody] UserPlayerDTO dto)
        {
            try
            {
                dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                _playerService.CreatePlayer(dto);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

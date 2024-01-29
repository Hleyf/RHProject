using Microsoft.AspNetCore.Mvc;
using RHP.API.Services;

namespace RHP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        PlayerService _playerService;
        public PlayerController(PlayerService playerService)
        {
            _playerService = playerService;
        }


        [HttpPost]
        public IActionResult CreatePlayerUser([FromBody] UserPlayerDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            else
            {
                dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                _playerService.CreatePlayer(dto);
                return Ok();


            }
        }
    }
}

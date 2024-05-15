using Microsoft.AspNetCore.Mvc;
using RHP.API.Services;

namespace RHP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayerController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public IActionResult GetPlayers()
        {
            return Ok(_playerService.GetAllPlayers());
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayer(int id)
        {
            return Ok(_playerService.GetPlayer(id));
        }



        [HttpPost]
        public IActionResult CreatePlayerUser([FromBody] UserPlayerDTO dto)
        {
            if (string.IsNullOrEmpty(dto.name) || string.IsNullOrEmpty(dto.email) || string.IsNullOrEmpty(dto.password))
            {
                return BadRequest();
            }

            try
            {
                
                _playerService.CreatePlayer(dto);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

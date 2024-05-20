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
        public async Task<IActionResult> GetPlayers()
        {
            return Ok(_playerService.GetAllPlayers());
        }

        [HttpGet("{Id}")]
        public IActionResult GetPlayer(int id)
        {
            return Ok(_playerService.GetPlayer(id));
        }



        [HttpPost]
        public async Task<IActionResult> CreatePlayerUser([FromBody] UserPlayerDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest();
            }

            try
            {
                
                await _playerService.CreatePlayer(dto);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

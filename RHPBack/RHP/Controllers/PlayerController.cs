using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RHP.Models;
using RHP.Repositories;
using System.Reflection;

namespace RHP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerRepository _playerRepository;

        public PlayerController(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }


        [HttpPost]
        public IActionResult CreatePlayerUser([FromBody] Player player)
        {
            if (player == null)
            {
                return BadRequest();
            }
            else
            {
                player.Password = BCrypt.Net.BCrypt.HashPassword(player.Password);
                _playerRepository.Add(player);
                return Ok(player.Id);


            }
        }
    }
}

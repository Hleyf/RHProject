using Microsoft.AspNetCore.Mvc;
using RHP.API.Services;
using RHP.Entities.Models.DTOs;

namespace RHP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly HallService _hallService;

        public HallController(HallService hallService )
        {
            _hallService = hallService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHalls()
        {
            var halls = await _hallService.GetAllHalls();
            return Ok(halls);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetHall(int id)
        {
            HallDTO hall = await _hallService.GetHall(id);
            return Ok(_hallService.GetHall(id));
        }

        [HttpPost]
        public IActionResult CreateHall([FromBody] HallDTO dto)
        {
            try
            {
                _hallService.CreateHall(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult DeleteHall(int id)
        {
            try
            {
                _hallService.DeleteHall(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

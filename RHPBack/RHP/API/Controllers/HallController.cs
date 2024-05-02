using Microsoft.AspNetCore.Mvc;
using RHP.API.Services;
using RHP.Entities.Models.DTOs;
using System.Security.Claims;

namespace RHP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly HallService _hallService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HallController(HallService hallService, IHttpContextAccessor httpContextAccessor)
        {
            _hallService = hallService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult GetHalls()
        {
            return Ok(_hallService.GetAllHalls());
        }

        [HttpGet("{id}")]
        public IActionResult GetHall(int id)
        {
            string userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
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

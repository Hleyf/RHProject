using Microsoft.AspNetCore.Mvc;
using RHP.API.Services;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;
using System.Security.Claims;

namespace RHP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly HallService _hallService;
        private readonly AuthenticationService _authenticationService;

        public HallController(HallService hallService, AuthenticationService authenticationService )
        {
            _hallService = hallService;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult GetHalls()
        {
            var halls = _hallService.GetAllHalls();
            return Ok(halls);
        }

        [HttpGet("{id}")]
        public IActionResult GetHall(int id)
        {
            HallDTO hall = _hallService.GetHall(id);
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

using Microsoft.AspNetCore.Mvc;

namespace RHP.API.Controllers
{
    [Route("api/Contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok();
        }

        [HttpGet("{Id}")]
        public IActionResult GetContact(int id)
        {
            return Ok();
        }

        [HttpGet("/remove/{Id}")]
        public IActionResult RemoveContact(int id)
        {
            return Ok();
        }

    }
}

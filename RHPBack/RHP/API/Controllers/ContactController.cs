using Microsoft.AspNetCore.Mvc;
using RHP.API.Services;

namespace RHP.API.Controllers
{
    [Route("api/Contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;
        private readonly AuthenticationService _authenticationService;

        public ContactController(ContactService contactService, AuthenticationService authenticationService)
        {
            _contactService = contactService;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _contactService.GetContacts();
            return Ok(contacts);
        }

        [HttpGet("{Id}")]
        public IActionResult GetContact(string id)
        {
            var contact = _contactService.GetContact(id);
            return Ok();
        }

        [HttpGet("/remove/{Id}")]
        public IActionResult RemoveContact(string id)
        {
            return Ok();
        }

    }
}

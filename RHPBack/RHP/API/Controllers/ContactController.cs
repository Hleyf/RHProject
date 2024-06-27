using Microsoft.AspNetCore.Mvc;
using RHP.API.Services;
using RHP.Entities.Models.DTOs;

namespace RHP.API.Controllers
{
    public  enum ContactRequestType
    {
        Send,
        Receive
    }

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
        public IActionResult GetContact(int id)
        {
            var contact = _contactService.GetContact(id);
            return Ok(contact);
        }

        //[HttpGet("requests")]
        //public async Task<IActionResult> GetContactRequests(ContactRequestType type)
        //{
        //    IEnumerable<ContactRequestDTO> requests;
        //    switch (type)
        //    {
        //        case ContactRequestType.Send:
        //            requests = await _contactService.GetSentContactRequests();
        //            return Ok(requests);
        //            break;
        //        case ContactRequestType.Receive:
        //            requests = await _contactService.GetReceivedContactRequests();
        //            return Ok(requests);
        //            break;
        //        default:
        //            return BadRequest();
        //    }
            
        //}

        [HttpPost]
        public async Task<IActionResult> RequestContact(ContactRequestDTO contactRequest)
        {
            await _contactService.RequestContact(contactRequest);
            return Ok();
        }

        [HttpDelete("/remove/{Id}")]
        public async Task<IActionResult> RemoveContact(int id)
        {
            await _contactService.GetContact(id);
            return NoContent() ;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContactRequest(ContactRequestDTO contactRequest)
        {
            await _contactService.UpdateContactRequest(contactRequest);
            return Ok();
        }

    }
}

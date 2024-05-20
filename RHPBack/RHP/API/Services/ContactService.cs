using AutoMapper;
using RHP.API.Repositories;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;

namespace RHP.API.Services
{
    public class ContactService
    {
        private readonly UserRepository _userRepository;
        private readonly AuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        public ContactService(UserRepository userRepository, AuthenticationService authenticationService, IMapper mapper)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<List<ContactDTO>> GetContacts()
        {
            string UserId = _authenticationService.GetLoggedUserId();
            return await _userRepository.GetContacts(UserId);
        }

        internal async Task<ContactDTO> GetContact(string id)
        {
            var contact = await _userRepository.GetContact(id);

            if(contact is null)
            {
                throw new Exception("Contact not found");
            }

            return contact;


        }

        internal async Task RemoveContact(string id)
        {
            string UserId = _authenticationService.GetLoggedUserId();
            
            //The contact needs to be removed on both ends. 
            User loggedUser = await _userRepository.GetByUserId(UserId);
            User contact = await _userRepository.GetByUserId(id);

            loggedUser.Contacts.Remove(contact);
            contact.Contacts.Remove(loggedUser);

            await _userRepository.SaveChangesAsync();


        }
    }
}

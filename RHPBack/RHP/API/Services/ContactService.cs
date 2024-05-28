using AutoMapper;
using RHP.API.Hubs;
using RHP.API.Repositories;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;

namespace RHP.API.Services
{
    public class ContactService
    {
        private readonly UserRepository _userRepository;
        private readonly AuthenticationService _authenticationService;
        private readonly ContactRequestRepository _contactRequestRepository;
        private readonly ContactHub _contactHub;
        private readonly IMapper _mapper;
        public ContactService(UserRepository userRepository, AuthenticationService authenticationService, ContactRequestRepository contactRequestRepository, ContactHub contactHub , IMapper mapper)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _contactRequestRepository = contactRequestRepository;
            _contactHub = contactHub;
            _mapper = mapper;
        }

        public async Task<List<Contact>> GetContacts()
        {
            string UserId = _authenticationService.GetLoggedUserId();
            return await _userRepository.GetContacts(UserId);
        }

        internal async Task<Contact> GetContact(string id)
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

        internal async Task RequestContact(ContactRequestDTO dto)
        {
            User fromUser = _authenticationService.GetLoggedUser();
            User toUser = await _userRepository.GetByUserId(dto.ToUserId);

            ValidateContactRequest(fromUser, toUser);

            ContactRequest contactRequest = CreateContactRequest(fromUser, toUser);

            _contactRequestRepository.Add(contactRequest);

            await NotifyContactHub(fromUser.Id, toUser.Id);
        }

        internal async Task<List<ContactRequestDTO>> GetReceivedContactRequests()
        {
            string UserId = _authenticationService.GetLoggedUserId();
            List<ContactRequest> requests = await _contactRequestRepository.GetAllAsyncByToUserId(UserId);
            return _mapper.Map<List<ContactRequestDTO>>(requests);
        }

        internal async Task<List<ContactRequestDTO>> GetSentContactRequests()
        {
            string UserId = _authenticationService.GetLoggedUserId();
            List<ContactRequest> requests = await _contactRequestRepository.GetAllAsyncByFromUserId(UserId);
            return _mapper.Map<List<ContactRequestDTO>>(requests);
        }

        private void ValidateContactRequest(User fromUser, User toUser)
        {
            if (fromUser.Contacts.Contains(toUser))
            {
                throw new Exception("Contact already exists");
            }

            if (fromUser.Id == toUser.Id)
            {
                throw new Exception("You can't add yourself as a contact");
            }
        }

        private ContactRequest CreateContactRequest(User fromUser, User toUser)
        {
            return new ContactRequest
            {
                From = fromUser,
                To = toUser,
                Status = ContactRequestStatus.Pending,
                CreatedAt = DateTime.Now
            };
        }

        private async Task NotifyContactHub(string fromUserId, string toUserId)
        {
            await _contactHub.ContactRequest(fromUserId, toUserId);
        }
    }
}

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
        private readonly ContactRepository _contactRepository;
        private readonly AuthenticationService _authenticationService;
        private readonly ContactHub _contactHub;
        private readonly IMapper _mapper;
        public ContactService(UserRepository userRepository, AuthenticationService authenticationService, ContactHub contactHub , IMapper mapper)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _contactHub = contactHub;
            _mapper = mapper;
        }

        public Task<List<ContactDTO>> GetContacts()
        {
            string UserId = _authenticationService.GetLoggedUserId();
            return _contactRepository.GetUserContactsDTO(UserId);
        }

        public async Task<ContactDTO> GetContact(int id)
        {
            Contact contact = await _contactRepository.GetByIdAsync(id);
            
            if(contact is null)
            {
                throw new Exception($"Contact {id} not found");
            }

            return _mapper.Map<ContactDTO>(contact);
        }

        internal void RemoveContact(int id)
        {
            string UserId = _authenticationService.GetLoggedUserId();

            _contactRepository.Remove(id);            

        }

        internal async Task RequestContact(ContactRequestDTO dto)
        {
            User fromUser = _authenticationService.GetLoggedUser();
            User toUser = await _userRepository.GetByUserId(dto.ToUserId);

            ValidateContactRequest(fromUser, toUser);

            Contact contactRequest = CreateContactRequest(fromUser, toUser);

            _contactRepository.Add(contactRequest);

            await NotifyContactHub(fromUser.Id, toUser.Id);
        }

        internal async Task UpdateContactRequest(ContactRequestDTO dto)
        {
            string UserId = _authenticationService.GetLoggedUserId();
            Contact contact = _contactRepository.GetById(dto.Id);

            if (contact is null)
            {
                throw new Exception($"Contact {dto.Id} not found");
            }
            
            if (dto.Status == ContactStatus.Accepted && contact.Recipient.Id != UserId)
            {
                throw new Exception("You can't accept a contact request that is not for you");
            }

            contact.Status = dto.Status;

            _contactRepository.Update(contact);

            await NotifyContactHub(contact.Requestor.Id, contact.Recipient.Id);
        }

        private void ValidateContactRequest(User fromUser, User toUser)
        {
            if (fromUser.Id == toUser.Id)
            {
                throw new Exception("You can't add yourself as a contact");
            }

            if (_contactRepository.ContactExist(fromUser.Id, toUser.Id))
            {
                throw new Exception("Contact already exists");
            }
        }

        private Contact CreateContactRequest(User fromUser, User toUser)
        {
            return new Contact
            {
                Requestor = fromUser,
                Recipient = toUser,
                Status = ContactStatus.Pending,
                CreatedAt = DateTime.Now
            };
        }

        private async Task NotifyContactHub(string fromUserId, string toUserId)
        {
            await _contactHub.ContactRequest(fromUserId, toUserId);
        }
    }
}

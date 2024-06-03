using Microsoft.AspNetCore.SignalR;
using RHP.API.Repositories;
using RHP.API.Services;
using RHP.API.Services.Interfaces;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;

namespace RHP.API.Hubs
{
    public class ContactHub : Hub<IContact>
    {
        private readonly AuthenticationService _authenticationService;
        private readonly ContactRepository _contactRepository;
        private readonly PlayerService _playerService;
        private readonly UserRepository _userRepository;

        public ContactHub(AuthenticationService authenticationService, ContactRepository contactRepository, PlayerService playerService, UserRepository userRepository) 
        {
            _authenticationService = authenticationService;
            _contactRepository = contactRepository;
            _playerService = playerService;
            _userRepository = userRepository;
        }

        public override async Task OnConnectedAsync()
        {
            string userId = _authenticationService.GetLoggedUserId();

            // Create user's group
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);

            // Notify all contacts that the user is now online
            await ContactStatusChanged(userId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            string userId = _authenticationService.GetLoggedUserId();

            // Notify all contacts that the user is now offline
            await ContactStatusChanged(userId);

            await base.OnDisconnectedAsync(ex);
        }

        internal async Task ContactStatusChanged(string userId)
        {
            User loggedUser = await _userRepository.GetByUserId(userId);

            if (loggedUser.loggedIn && loggedUser.Status.Equals(UserStatus.Online)) 
            {
                // Get the user's contacts
                List<User> contacts = await _contactRepository.GetContactUserListAsync(userId);

                //Get the user's player name
                string name = await _playerService.GetPlayerNameByUserId(userId);

                ContactUpdateDTO contactUpdate = new ContactUpdateDTO
                {
                    UserId = userId,
                    Name = name
                };

                // Notify each contact
                foreach (var user in contacts)
                {
                    if (user.loggedIn && user.Status.Equals(UserStatus.Online))
                    {
                        //Notice we are only notifiying the contacts that are logged in, the updated contact list needs to be fetched by the client
                        await Clients.Group(userId).ContactStatusChanged(contactUpdate);
                    }
                }
            }
        }

        internal async Task ContactRequest (string fromId, string toId)
        {
            if(await _userRepository.IsUserLoggedIn(toId))
            {
                string name = await _playerService.GetPlayerNameByUserId(fromId);
                
                ContactUpdateDTO contactRequest = new ContactUpdateDTO
                {
                    UserId = fromId,
                    Name = name
                };

                await Clients.Group(fromId).ContactRequestReceived(contactRequest);
            }
        }
    }
}

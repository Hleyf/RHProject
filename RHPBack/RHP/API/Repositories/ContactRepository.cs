using Microsoft.EntityFrameworkCore;
using RHP.Data;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;

namespace RHP.API.Repositories
{
    public class ContactRepository : GenericRepository<Contact>
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ContactDTO>> GetUserContactsDTO(string userId)
        {
            return await _context.Contacts
                .Where(c => EF.Property<string>(c, "RequestorId") == userId || EF.Property<string>(c, "RecipientId") == userId) //RequesttorId is a shadow property so it can't be accessed directly
                .OrderBy(c => c.Status == ContactStatus.Pending ? 0 : 1) // Pending contacts first
                .ThenByDescending(c => c.Status == ContactStatus.Accepted && (EF.Property<string>(c, "RequestorId") == userId ? c.Recipient.loggedIn : c.Requestor.loggedIn) ? 0 : 1) // Then accepted contacts where the other user is logged in
                .ThenByDescending(c => c.Status == ContactStatus.Accepted ? (EF.Property<string>(c, "RequestorId") == userId ? c.Recipient.lastLogin : c.Requestor.lastLogin) : DateTime.MinValue) // Then the rest of the accepted contacts, ordered by lastLogin
                .Select(c => new ContactDTO //Selecting only the necessary fields to avoid loading the whole entity
                {
                    UserId = EF.Property<string>(c, "RequestorId") == userId ? EF.Property<string>(c, "RecipientId") : EF.Property<string>(c, "RequestorId"),
                    Name = EF.Property<string>(c, "RequestorId") == userId ? c.Recipient.Player.Name : c.Requestor.Player.Name,
                    Email = EF.Property<string>(c, "RequestorId") == userId ? c.Recipient.Email : c.Requestor.Email,
                    loggedIn = EF.Property<string>(c, "RequestorId") == userId ? c.Recipient.loggedIn : c.Requestor.loggedIn,
                    userStatus = EF.Property<string>(c, "RequestorId") == userId ? c.Recipient.Status : c.Requestor.Status,
                    lastLogin = EF.Property<string>(c, "RequestorId") == userId ? c.Recipient.lastLogin : c.Requestor.lastLogin
                })
                .ToListAsync();
        }
        public async Task<Contact?> GetContactByUsersIds(string loggedUserId, string userId)
        {
            return await _context.Contacts
                .FirstOrDefaultAsync(c =>
                    (EF.Property<string>(c, "RequestorId") == loggedUserId && EF.Property<string>(c, "RecipientId") == userId) ||
                    (EF.Property<string>(c, "RequestorId") == userId && EF.Property<string>(c, "RecipientId") == loggedUserId));
        }

        public async Task<List<User>> GetContactUserListAsync (string userId)
        {
            return await _context.Contacts
                .Where(c => EF.Property<string>(c, "RequestorId") == userId || EF.Property<string>(c, "RecipientId") == userId)
                .Select(c => EF.Property<string>(c, "RequestorId") == userId ? c.Recipient : c.Requestor)
                .ToListAsync();
        }

        public void RemoveContactByUsersIds(string loggedUserId, string userId) 
        {
            Contact? contact = GetContactByUsersIds(loggedUserId, userId).Result;

            if(contact is null)
            {
                throw new Exception("No contact found");
            }
            else
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
            }
        }

        public bool ContactExist(string loggedUserId, string userId)
        {
            return _context.Contacts.Any(c =>
                (EF.Property<string>(c, "RequestorId") == loggedUserId && EF.Property<string>(c, "RecipientId") == userId) ||
                (EF.Property<string>(c, "RequestorId") == userId && EF.Property<string>(c, "RecipientId") == loggedUserId));
        }


    }
}

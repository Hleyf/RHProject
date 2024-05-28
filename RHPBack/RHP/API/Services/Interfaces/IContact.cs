using RHP.Entities.Models.DTOs;

namespace RHP.API.Services.Interfaces
{
    public interface IContact
    {
        Task ContactStatusChanged(ContactUpdateDTO contactUpdate);
        Task ContactRequestReceived(ContactUpdateDTO contactRequest);
        Task ReceiveStatus();

    }
}

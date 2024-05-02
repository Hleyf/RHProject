using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;

public interface IAuthenticationService
{
    string Login(UserLoginDTO userLoginDTO);
    Player getLoggedPlayer();
}

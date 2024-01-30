using RHP.Entities.Models.DTOs;

public interface IAuthenticationService
{
    string Login(UserLoginDTO userLoginDTO);

    string RefreshToken();
}

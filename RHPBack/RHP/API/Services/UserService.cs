using AutoMapper;
using RHP.API.Repositories;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;

namespace RHP.API.Services
{ 
    public interface IUserService
    {
        UserDTO GetUser(int id);
        User CreateUser(UserPlayerDTO dto);
    }
    public class UserService: IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserService(IMapper mapper, UserRepository userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDTO GetUser(int id)
        {
            User user = _userRepository.GetById(id);

            return _mapper.Map<UserDTO>(user);
        }

        public User CreateUser(UserPlayerDTO dto)
        {
            try {
                User user = _mapper.Map<User>(dto);
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                _userRepository.Add(user);
                return user;
            } catch (Exception ex)
            {
                throw new Exception("Could not create user", ex);
            }
        }

        public UserDTO CreateUserToDTO(UserPlayerDTO dto)
        {
            User user = _mapper.Map<User>(dto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _userRepository.Add(user);
            return _mapper.Map<UserDTO>(user);
        }

    }
}

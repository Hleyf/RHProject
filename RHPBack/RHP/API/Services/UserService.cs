using AutoMapper;
using RHP.API.Repositories;
using RHP.Entities.Models;

namespace RHP.API.Services
{
    public class UserService
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
            User user = _mapper.Map<User>(dto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _userRepository.Add(user);
            return user;
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

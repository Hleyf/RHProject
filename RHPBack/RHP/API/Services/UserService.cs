using AutoMapper;
using RHP.API.Repositories;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;
using System.ComponentModel.DataAnnotations;

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

        public User CreateUser(UserLoginDTO dto)
        {
            try {
                User user = new User
                    {
                    email = dto.email,
                    password = BCrypt.Net.BCrypt.HashPassword(dto.password),
                    role = UserRole.Player

                };

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
            user.password = BCrypt.Net.BCrypt.HashPassword(dto.password);
            _userRepository.Add(user);
            return _mapper.Map<UserDTO>(user);
        }

    }
}

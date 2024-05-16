﻿using AutoMapper;
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
                    Email = dto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    Role = UserRole.Player

                };

                _userRepository.Add(user);
                return user;
            } catch (Exception ex)
            {
                throw new Exception("Could not create User", ex);
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

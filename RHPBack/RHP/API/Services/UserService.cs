﻿using AutoMapper;
using RHP.API.Repositories;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;

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

        public async Task<UserDTO> GetUserDTO(string id)
        {
            User user = await _userRepository.GetByUserId(id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<User> GetUserById(string id)
        {
            User user = await _userRepository.GetByUserId(id);

            if (user is null) 
            {
                throw new Exception("User not found");
            }

            return user;
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email) ?? throw new Exception("User not found");

        }
        public async Task<User> CreateUser(UserPlayerDTO dto)
        {
            try {
                User user = new User
                {
                    Id = "#" + dto.Name.Substring(0, Math.Min(5, dto.Name.Length)) + new Random().Next(1000, 9999).ToString(),
                    Email = dto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    Role = UserRole.Player,
                    lastLogin = DateTime.Now
                };
                
                await _userRepository.CreateUser(user);
                return user;
            } catch (Exception ex)
            {
                throw new Exception("Could not create User", ex);
            }
        }

        public async Task<UserDTO> CreateUserToDTO(UserPlayerDTO dto)
        {
            User user = _mapper.Map<User>(dto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            await _userRepository.CreateUser(user);
            return _mapper.Map<UserDTO>(user);
        }

        internal void UpdateUser(User user)
        {
            User existingUser = _userRepository.GetUserByEmail(user.Email) ?? throw new Exception("User not found");

            _mapper.Map(user, existingUser);

            _userRepository.Update(existingUser);
        } 
    }
}

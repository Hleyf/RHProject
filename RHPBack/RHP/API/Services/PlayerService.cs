﻿using AutoMapper;
using RHP.API.Repositories;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;
namespace RHP.API.Services
{
    public class PlayerService
    {
        private readonly IMapper _mapper;
        private readonly PlayerRepository _playerRepository;
        private readonly UserService _userService;
        private readonly HallRepository _hallRepository;

        public PlayerService(IMapper mapper, PlayerRepository playerRepository, UserService userService, HallRepository hallRepository)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
            _userService = userService;
            _hallRepository = hallRepository;
        }

        public PlayerDTO GetPlayer(int id)
        {
            Player player = _playerRepository.GetById(id);

            return _mapper.Map<PlayerDTO>(player);
        }

        public IEnumerable<PlayerDTO> GetAllPlayers()
        {
            IEnumerable<Player> players = _playerRepository.GetAll();

            return _mapper.Map<IEnumerable<PlayerDTO>>(players);
        }

        public void CreatePlayer(UserPlayerDTO dto)
        {
            UserLoginDTO userDTO = new UserLoginDTO { Email = dto.Email, Password = dto.Password  };
            User savedUser = _userService.CreateUser(userDTO);

            Player player = new Player { Name = dto.Name, User = savedUser };
            player.User.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            _playerRepository.Add(player);
        }

        public void UpdatePlayer(PlayerDTO playerDTO)
        {
            Player player = _mapper.Map<Player>(playerDTO);
            if (playerDTO.HallIds != null && playerDTO.HallIds.Any())
            {
                player.Halls = _hallRepository.GetByIdWithIncludes(playerDTO.HallIds);
            }
            _playerRepository.Update(player);
        }

        public PlayerDTO GetPlayerById(int id)
        {
            Player player = _playerRepository.GetById(id);

            return _mapper.Map<PlayerDTO>(player);
        }

        public PlayerDTO GetPlayerByName(string name)
        {
            Player? player = _playerRepository.GetPlayerByName(name);

            if (player == null)
            {
                throw new Exception("Player not found");
            }

            return _mapper.Map<PlayerDTO>(player);
        }

        public Player GetPlayerByUserId(int userId)
        {
            Player? player = _playerRepository.GetPlayerByUserId(userId);
            if (player == null)
            {
                throw new Exception("Player not found");
            }
            return player;
        }
    }
}

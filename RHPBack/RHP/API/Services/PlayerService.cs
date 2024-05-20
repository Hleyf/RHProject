using AutoMapper;
using RHP.API.Repositories;
using RHP.Entities.Models;
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

        public async Task<PlayerDTO> GetPlayer(int id)
        {
            Player player = await _playerRepository.GetByIdAsync(id);

            return _mapper.Map<PlayerDTO>(player);
        }

        public async Task<IEnumerable<PlayerDTO>> GetAllPlayers()
        {
            IEnumerable<Player> players = await _playerRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<PlayerDTO>>(players);
        }

        public async Task CreatePlayer(UserPlayerDTO dto)
        {
            User savedUser = await _userService.CreateUser(dto);

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

        //public PlayerDTO GetPlayerById(int id)
        //{
        //    Player player = _playerRepository.GetById(id);

        //    return _mapper.Map<PlayerDTO>(player);
        //}

        public async Task<PlayerDTO> GetPlayerByName(string name)
        {
            Player? player = await _playerRepository.GetPlayerByName(name);

            if (player is null)
            {
                throw new Exception("Player not found");
            }

            return _mapper.Map<PlayerDTO>(player);
        }

        public async Task<Player> GetPlayerByUserId(string userId)
        {
            Player? player = await _playerRepository.GetPlayerByUserId(userId);
            
            if (player is null)
            {
                throw new Exception("Player not found");
            }
            return player;
        }
    }
}

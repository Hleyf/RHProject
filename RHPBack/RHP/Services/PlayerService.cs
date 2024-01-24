using AutoMapper;
using RHP.Models;
using RHP.Repositories;
using System.Linq;

namespace RHP.Services
{
    public class PlayerService
    {
        private readonly IMapper _mapper;
        private readonly PlayerRepository _playerRepository;
        private readonly HallRepository _hallRepository;

        public PlayerService(IMapper mapper, PlayerRepository playerRepository, HallRepository hallRepository)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
            _hallRepository = hallRepository;
        }

        public PlayerDTO GetPlayer(int id)
        {
            Player player = _playerRepository.GetById(id);
            return _mapper.Map<PlayerDTO>(player);
        }

        public void CreatePlayer(PlayerDTO playerDTO)
        {
            Player player = _mapper.Map<Player>(playerDTO);

            _playerRepository.Add(player);
        }

        public void UpdatePlayer(PlayerDTO playerDTO)
        {
            Player player = _mapper.Map<Player>(playerDTO);
            if (playerDTO.HallIds != null && playerDTO.HallIds.Any()) { 
                player.Halls = _hallRepository.GetByIdWithIncludes(playerDTO.HallIds);
            }
            _playerRepository.Update(player);
        }
    }
}

using AutoMapper;
using RHP.API.Repositories;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;

namespace RHP.API.Services
{
    public class HallService
    {
        private readonly HallRepository _hallRepository;
        private readonly IMapper _mapper;


        public HallService(IMapper mapper, HallRepository hallRepository, AuthenticationService authenticationService)
        {
            _hallRepository = hallRepository;
            _mapper = mapper;
        }

        public void CreateHall(HallDTO dto)
        {
            Hall hall = _mapper.Map<Hall>(dto);

            _hallRepository.Add(hall);
        }

        public async Task<HallDTO> GetHall(int id)
        {
            Hall hall = await _hallRepository.GetWithRelationships(id);

            return _mapper.Map<HallDTO>(hall);
            
        }

        public async Task<IEnumerable<HallDTO>> GetAllHalls()
        {
            IEnumerable<Hall> halls = await _hallRepository.GetAllWithRelationships();
            return _mapper.Map<IEnumerable<HallDTO>>(halls);


        }

        public void UpdateHall(HallDTO hallDTO)
        {
            Hall hall = _mapper.Map<Hall>(hallDTO);

            _hallRepository.Update(hall);
        }

        public void DeleteHall(int id) { 
        
            _hallRepository.Remove(id); 
        }



    }
}

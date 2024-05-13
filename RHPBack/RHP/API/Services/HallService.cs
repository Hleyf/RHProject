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
        private readonly AuthenticationService _authenticationService;


        public HallService(IMapper mapper, HallRepository hallRepository, AuthenticationService authenticationService)
        {
            _hallRepository = hallRepository;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public void CreateHall(HallDTO dto)
        {
            Hall hall = _mapper.Map<Hall>(dto);

            _hallRepository.Add(hall);
        }

        public HallDTO GetHall(int id)
        {
            Hall hall = _hallRepository.GetWithRelationships(id);

            return _mapper.Map<HallDTO>(hall);
            
        }

        public IEnumerable<HallDTO> GetAllHalls()
        {
            IEnumerable<Hall> halls = _hallRepository.GetAllWithRelationships();
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

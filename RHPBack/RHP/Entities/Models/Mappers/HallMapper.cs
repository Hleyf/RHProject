using AutoMapper;
using RHP.Entities.Models.DTOs;

namespace RHP.Entities.Models.Mappers
{
    public class HallMapper : Profile
    {

        public HallMapper()
        {
            CreateMap<Hall, HallDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.gameMasterId, opt => opt.MapFrom(src => src.gameMaster.id))
                .ForMember(dest => dest.players, opt => opt.MapFrom(src => src.players));
        }
    }
}

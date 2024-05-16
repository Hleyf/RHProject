using AutoMapper;
using RHP.Entities.Models.DTOs;

namespace RHP.Entities.Models.Mappers
{
    public class HallMapper : Profile
    {

        public HallMapper()
        {
            CreateMap<Hall, HallDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.GameMasterId, opt => opt.MapFrom(src => src.GameMaster.Id))
                .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players));
        }
    }
}

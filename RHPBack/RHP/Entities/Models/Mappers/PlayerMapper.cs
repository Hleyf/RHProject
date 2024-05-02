using AutoMapper;

namespace RHP.Entities.Models.Mappers
{
    public class PlayerMapper : Profile
    {

        public PlayerMapper()
        {
            CreateMap<Player, PlayerDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .AfterMap((src, dest) =>
                {
                    int[] arr = src.Halls.Select(h => h.Id).ToArray();
                    dest.HallIds = arr;
                });
        }
    }
}

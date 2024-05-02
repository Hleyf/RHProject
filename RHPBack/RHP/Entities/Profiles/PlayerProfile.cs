using AutoMapper;
using RHP.Entities.Models;

public class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<Player, PlayerDTO>()
            .ForMember(dest => dest.HallIds, opt => opt.MapFrom(src => src.Halls != null ? src.Halls.Select(h => h.Id).ToArray() : null));
        CreateMap<PlayerDTO, Player>();
        CreateMap<UserPlayerDTO, Player>()
            .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name != null ? src.Name : null));
        CreateMap<Player, UserPlayerDTO>()
            .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name != null ? src.Name : null));
    }
}

using AutoMapper;
using RHP.Models;

public class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<Player, PlayerDTO>()
            .ForMember(dest => dest.HallIds, opt => opt.MapFrom(src => src.Halls != null ? src.Halls.Select(h => h.Id).ToArray() : null));
        CreateMap<PlayerDTO, Player>();
        CreateMap<PlayerCreateDTO, Player>();
    }
}

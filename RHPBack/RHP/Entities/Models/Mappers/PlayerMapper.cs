using AutoMapper;

namespace RHP.Entities.Models.Mappers
{
    public class PlayerMapper : Profile
    {

        public PlayerMapper()
        {
            CreateMap<Player, PlayerDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.user.id))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.user.email))
                .AfterMap((src, dest) =>
                {
                    int[] arr = src.halls.Select(h => h.id).ToArray();
                    dest.hallIds = arr;
                });
            CreateMap<PlayerDTO, Player>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
                .ForPath(dest => dest.user.id, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
                .ForPath(dest => dest.user.email, opt => opt.MapFrom(src => src.email));

            CreateMap<UserPlayerDTO, Player>()
                .ForMember(des => des.name, opt => opt.MapFrom(src => src.name != null ? src.name : null));

            CreateMap<Player, UserPlayerDTO>()
                .ForMember(des => des.name, opt => opt.MapFrom(src => src.name != null ? src.name : null))
                .ForMember(des => des.email, opt => opt.MapFrom(src => src.user.email != null ? src.user.email : null))
                .ForMember(des => des.password, opt => opt.Ignore());
        }
    }
}

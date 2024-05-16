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
            CreateMap<PlayerDTO, Player>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<UserPlayerDTO, Player>()
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name != null ? src.Name : null));

            CreateMap<Player, UserPlayerDTO>()
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name != null ? src.Name : null))
                .ForMember(des => des.Email, opt => opt.MapFrom(src => src.User.Email != null ? src.User.Email : null))
                .ForMember(des => des.Password, opt => opt.Ignore());
        }
    }
}

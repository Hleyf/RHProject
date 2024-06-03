using AutoMapper;
using RHP.Entities.Models.DTOs;

namespace RHP.Entities.Models.Mappers
{
    public class ContactRequestMapper : Profile
    {
        public ContactRequestMapper()
        {
            CreateMap<ContactRequest, ContactRequestDTO>()
                .ForMember(dest => dest.FromUserId, opt => opt.MapFrom(src => src.FromUser.Id))
                .ForMember(dest => dest.ToUserId, opt => opt.MapFrom(src => src.ToUser.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<ContactRequestDTO, ContactRequest>()
                .ForMember(dest => dest.FromUser.Id, opt => opt.MapFrom(src => src.FromUserId))
                .ForMember(dest => dest.ToUser.Id, opt => opt.MapFrom(src => src.ToUserId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}

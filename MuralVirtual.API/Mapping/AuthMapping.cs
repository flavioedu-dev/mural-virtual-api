using AutoMapper;
using MuralVirtual.API.Models.Auth;
using MuralVirtual.Domain.DTOs.Auth;
using MuralVirtual.Domain.Entities;

namespace MuralVirtual.API.Mapping;

public class AuthMapping : Profile
{
    public AuthMapping()
    {
        CreateMap<RegisterModel, RegisterDTO>();

        CreateMap<RegisterDTO, User>()
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<(User user, string message), RegisterResponseDTO>()
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.message))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.user.Id));
    }
}
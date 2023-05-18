using AutoMapper;
using BirdClubAPI.Domain.DTOs.Response.User;
using BirdClubAPI.Domain.DTOs.View.Auth;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class UserModule
    {
        public static void ConfigUserModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<User, AuthViewModel>()
                .ForMember(dest => dest.JwtToken, opts => opts.Ignore())
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.Id))
                .ReverseMap();
            mc.CreateMap<User, UserResponseModel>().ReverseMap();
        }
    }
}

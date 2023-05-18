using AutoMapper;
using BirdClubAPI.Domain.DTOs.Request.Member;
using BirdClubAPI.Domain.DTOs.Response.Member;
using BirdClubAPI.Domain.DTOs.View.Member;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class MemberModule
    {
        public static void ConfigMemberModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Member, MemberResponseModel>()
                .ForMember(dest => dest.MemberId, opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(src => src.User.DisplayName))
                .ReverseMap();

            mc.CreateMap<Member, UpdateMemberRequestModel>()
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(src => src.User.DisplayName))
                .ReverseMap();
            mc.CreateMap<Member, MemberProfileResponseModel>().ReverseMap();
            mc.CreateMap<MemberProfileResponseModel, MemberViewModel>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(src => src.User.DisplayName))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.UserType, opts => opts.MapFrom(src => src.User.UserType))
                .ReverseMap();
        }
    }
}

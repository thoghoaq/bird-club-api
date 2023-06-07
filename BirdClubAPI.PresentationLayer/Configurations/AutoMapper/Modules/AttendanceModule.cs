using AutoMapper;
using BirdClubAPI.Domain.DTOs.Request.Activity;
using BirdClubAPI.Domain.DTOs.Response.Activity;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class AttendanceModule
    {
        public static void ConfigAttendanceModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Attendance, AttendanceActivityRequestModel>().ReverseMap();
            mc.CreateMap<AttendanceRequest, AttendanceRequestRm>()
                .ForMember(dest => dest.MemberId, opts => opts.MapFrom(src => src.MemberId))
                .ForMember(dest => dest.Avatar, opts => opts.MapFrom(src => src.Member.Avatar))
                .ForMember(dest => dest.DisplayName, opts => opts.MapFrom(src => src.Member.User.DisplayName))
                .ForMember(dest => dest.UserType, opts => opts.MapFrom(src => src.Member.User.UserType))
                .ForMember(dest => dest.RequestTime, opts => opts.MapFrom(src => src.RequestTime))
                .ReverseMap();
        }
    }
}

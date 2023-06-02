using AutoMapper;
using BirdClubAPI.Domain.DTOs.Request.Activity;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class AttendanceModule
    {
        public static void ConfigAttendanceModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Attendance, AttendanceActivityRequestModel>().ReverseMap();
            
        }
    }
}

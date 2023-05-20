using AutoMapper;
using BirdClubAPI.Domain.DTOs.Request.Activity;
using BirdClubAPI.Domain.DTOs.Response.Activity;
using BirdClubAPI.Domain.DTOs.View.Acitivity;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class ActivityModule
    {
        public static void ConfigActivityModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Activity, ActivityResponseModel>().ReverseMap();
            mc.CreateMap<Activity, CreateActivityRequestModel>().ReverseMap();
            mc.CreateMap<ActivityResponseModel, AcitivityCreateViewModel>().ReverseMap();
            mc.CreateMap<ActivityResponseModel, AcitivityViewModel>().ReverseMap();
        }
    }
}

using AutoMapper;
using BirdClubAPI.Domain.DTOs.Request.Bird;
using BirdClubAPI.Domain.DTOs.Response.Bird;
using BirdClubAPI.Domain.DTOs.View.Bird;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class BirdModule
    {
        public static void ConfigBirdModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<BirdResponseModel, BirdViewModel>().ReverseMap();
            mc.CreateMap<Bird, AddBirdRequestModel>().ReverseMap();
            mc.CreateMap<BirdResponseModel, AddBirdViewModel>().ReverseMap();
            mc.CreateMap<BirdResponseModel, Bird>().ReverseMap();

        }
    }
}

using AutoMapper;
using BirdClubAPI.Domain.DTOs.Response.Newsfeed;
using BirdClubAPI.Domain.DTOs.View.Newsfeed;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class NewsfeedModule
    {
        public static void ConfigNewsfeedModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Newsfeed, NewsfeedResponseModel>().ReverseMap();
            mc.CreateMap<NewsfeedResponseModel, NewsfeedViewModel>().ReverseMap();
        }
    }
}

using AutoMapper;
using BirdClubAPI.Domain.Commons.Enums;
using BirdClubAPI.Domain.DTOs.Response.Blog;
using BirdClubAPI.Domain.DTOs.Response.Newsfeed;
using BirdClubAPI.Domain.DTOs.View.Newsfeed;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class NewsfeedModule
    {
        public static void ConfigNewsfeedModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Newsfeed, NewsfeedResponseModel>()
                .ForMember(dest => dest.NewsfeedType, opts => opts.MapFrom(src => src.Blog != null ? NewsfeedTypeEnum.BLOG : NewsfeedTypeEnum.RECORD))
                .ReverseMap();
            mc.CreateMap<NewsfeedViewModel, NewsfeedResponseModel>().ReverseMap();
            mc.CreateMap<Newsfeed, NewsfeedViewModel>().ReverseMap();
        }
    }
}

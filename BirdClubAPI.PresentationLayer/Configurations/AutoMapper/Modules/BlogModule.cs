using AutoMapper;
using BirdClubAPI.Domain.DTOs.Response.Blog;
using BirdClubAPI.Domain.DTOs.View.Blog;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class BlogModule
    {
        public static void ConfigBlogModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Blog, BlogResponseModel>()
                .ForMember(dest => dest.LikeCount, otps => otps.Ignore())
                .ForMember(dest => dest.IsLiked, otps => otps.Ignore())
                .ReverseMap();
            mc.CreateMap<BlogDetailResponseModel, BlogViewModel>().ReverseMap();
            mc.CreateMap<BlogDetailResponseModel, Blog>()
                .ForMember(dest => dest.NewsfeedId, opts => opts.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}

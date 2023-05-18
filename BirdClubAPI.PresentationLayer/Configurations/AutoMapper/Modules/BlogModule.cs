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
            mc.CreateMap<Blog, BlogResponseModel>().ReverseMap();
            mc.CreateMap<BlogDetailResponseModel, BlogViewModel>().ReverseMap();
        }
    }
}

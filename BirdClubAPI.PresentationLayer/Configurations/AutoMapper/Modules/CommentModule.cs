using AutoMapper;
using BirdClubAPI.Domain.DTOs.Response.Comment;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.configurations.AutoMapper.Modules
{
    public static class CommentModule
    {
        public static void ConfigCommentModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Comment, CommentRm>().ReverseMap();
        }
    }
}

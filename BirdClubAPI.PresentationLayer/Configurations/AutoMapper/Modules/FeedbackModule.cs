using AutoMapper;
using BirdClubAPI.Domain.DTOs.Response.Feedback;
using BirdClubAPI.Domain.DTOs.View.Feedback;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class FeedbackModule
    {
        public static void ConfigFeedbackModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<FeedbackResponseModel, FeedbackViewModel>().ReverseMap();
            mc.CreateMap<Feedback, FeedbackResponseModel>().ReverseMap();
        }
    }
}

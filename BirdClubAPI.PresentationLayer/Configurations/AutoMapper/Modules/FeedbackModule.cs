using AutoMapper;
using BirdClubAPI.Domain.DTOs.Request.Feedback;
using BirdClubAPI.Domain.DTOs.Response.Bird;
using BirdClubAPI.Domain.DTOs.Response.Feedback;
using BirdClubAPI.Domain.DTOs.View.Bird;
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
            mc.CreateMap<Feedback, CreateFeedbackRequestModel>().ReverseMap();
            mc.CreateMap<FeedbackResponseModel, CreateFeedbackViewModel>().ReverseMap();
        }
    }
}

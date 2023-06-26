using AutoMapper;
using BirdClubAPI.Domain.DTOs.Request.Record;
using BirdClubAPI.Domain.DTOs.Response.Bird;
using BirdClubAPI.Domain.DTOs.Response.Record;
using BirdClubAPI.Domain.DTOs.View.Record;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class BirdModule
    {
        public static void ConfigBirdModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<RecordResponseModel, RecordViewModel>().ReverseMap();
            mc.CreateMap<Record, AddRecordRequestModel>()
                .ForMember(dest => dest.OwnerId, opts => opts.Ignore())
                .ReverseMap();
            mc.CreateMap<RecordResponseModel, AddRecordViewModel>().ReverseMap();
            mc.CreateMap<BirdResponseModel, Bird>().ReverseMap();

        }
    }
}

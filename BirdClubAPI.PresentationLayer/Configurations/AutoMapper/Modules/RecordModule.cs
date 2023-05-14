using AutoMapper;
using BirdClubAPI.Domain.DTOs.Response.Record;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules
{
    public static class RecordModule
    {
        public static void ConfigRecordModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Record, RecordResponseModel>()
                .ForMember(dest => dest.BirdName, opts => opts.MapFrom(src => src.Bird.Name))
                .ForMember(dest => dest.Species, opts => opts.MapFrom(src => src.Bird.Species))
                .ReverseMap();
        }
    }
}

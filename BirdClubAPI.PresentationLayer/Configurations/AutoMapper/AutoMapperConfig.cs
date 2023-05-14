using AutoMapper;
using BirdClubAPI.PresentationLayer.Configurations.AutoMapper.Modules;

namespace BirdClubAPI.BusinessLayer.Configurations.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            MapperConfiguration mapperConfiguration = new(mc =>
            {
                mc.ConfigNewsfeedModule();
                mc.ConfigBlogModule();
                mc.ConfigMemberModule();
                mc.ConfigRecordModule();
                mc.ConfigUserModule();
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}

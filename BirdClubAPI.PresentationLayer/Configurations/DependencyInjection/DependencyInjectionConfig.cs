using BirdClubAPI.BusinessLayer.Services.Activity;
using BirdClubAPI.BusinessLayer.Services.Auth;
using BirdClubAPI.BusinessLayer.Services.Bird;
using BirdClubAPI.BusinessLayer.Services.Feedback;
using BirdClubAPI.BusinessLayer.Services.Member;
using BirdClubAPI.BusinessLayer.Services.Newsfeed;
using BirdClubAPI.BusinessLayer.Services.Record;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.DataAccessLayer.Repositories.Activity;
using BirdClubAPI.DataAccessLayer.Repositories.Bird;
using BirdClubAPI.DataAccessLayer.Repositories.Feedback;
using BirdClubAPI.DataAccessLayer.Repositories.Member;
using BirdClubAPI.DataAccessLayer.Repositories.Newsfeed;
using BirdClubAPI.DataAccessLayer.Repositories.Record;
using BirdClubAPI.DataAccessLayer.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace BirdClubAPI.Core.DependencyInjection
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, BirdClubContext>(opts => opts.UseSqlServer(
                configuration.GetConnectionString("BirdClub")!
            ));

            services.AddScoped<INewsfeedService, NewsfeedService>();
            services.AddTransient<INewsfeedRepository, NewsfeedRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddScoped<IMemberService, MemberService>();
            services.AddTransient<IMemberRepository, MemberRepository>();

            services.AddScoped<IActivityService, ActivityService>();
            services.AddTransient<IActivityRepository, ActivityRepository>();

            services.AddScoped<IRecordService, RecordService>();
            services.AddTransient<IRecordRepository, RecordRepository>();

            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();

            services.AddScoped<IBirdService, BirdService>();
            services.AddTransient<IBirdRepository, BirdRepository>();

            return services;
        }
    }
}

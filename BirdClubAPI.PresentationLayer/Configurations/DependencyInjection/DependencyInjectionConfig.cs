using BirdClubAPI.BusinessLayer.Services.Activity;
using BirdClubAPI.BusinessLayer.Services.Auth;
using BirdClubAPI.BusinessLayer.Services.Member;
using BirdClubAPI.BusinessLayer.Services.Newsfeed;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.DataAccessLayer.Repositories.Activity;
using BirdClubAPI.DataAccessLayer.Repositories.Member;
using BirdClubAPI.DataAccessLayer.Repositories.Newsfeed;
using BirdClubAPI.DataAccessLayer.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace BirdClubAPI.Core.DependencyInjection
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, BirdClubContext>(opts => opts.UseSqlServer(
                configuration.GetConnectionString("BirdClub")
            ));

            services.AddScoped<INewsfeedService, NewsfeedService>();
            services.AddTransient<INewsfeedRepository, NewsfeedRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddScoped<IMemberService, MemberService>();
            services.AddTransient<IMemberRepository, MemberRepository>();

            services.AddScoped<IActivityService, ActivityService>();
            services.AddTransient<IActivityRepository, ActivityRepository>();

            return services;
        }
    }
}

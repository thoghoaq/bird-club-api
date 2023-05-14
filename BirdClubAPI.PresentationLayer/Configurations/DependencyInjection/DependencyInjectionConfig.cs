using BirdClubAPI.BusinessLayer.Services.Auth;
using BirdClubAPI.BusinessLayer.Services.Newsfeed;
using BirdClubAPI.DataAccessLayer.Context;
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

            return services;
        }
    }
}

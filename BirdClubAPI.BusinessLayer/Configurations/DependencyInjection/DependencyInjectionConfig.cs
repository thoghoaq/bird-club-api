using BirdClubAPI.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BirdClubAPI.BusinessLayer.Configurations.DependencyInjection
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, BirdClubContext>(opts => opts.UseSqlServer(
                configuration.GetConnectionString("BirdClub")
            ));

            return services;
        }
    }
}

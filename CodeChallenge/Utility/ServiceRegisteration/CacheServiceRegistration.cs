using CodeChallenge.Utility.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChallenge.Utility.ServiceRegisteration
{
    public static class CacheServiceRegistration
    {
        public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ICacheService, CacheService>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });
            return services;
        }
    }
}

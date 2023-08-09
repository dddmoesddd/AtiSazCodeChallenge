using CodeChallenge.Infrastructure.Repositories;
using CodeChallenge.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChallenge.Utility.ServiceRegisteration
{
    public static class InfrastructureServiceRegisteration
    {
        public static IServiceCollection AddInfrastructureServiceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            services.AddScoped<IMongoRepository, MongoRepository>();
            return services;
        }
    }
}

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CodeChallenge.Utility.ServiceRegisteration
{
    public static class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddApplicationServiceServiceServices(this IServiceCollection services)
        {

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {

                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));



            });
            return services;
        }
    }
}

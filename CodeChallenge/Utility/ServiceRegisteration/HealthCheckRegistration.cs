using CodeChallenge.Utility.CustomeHealthCheck;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;

namespace CodeChallenge.Utility.ServiceRegisteration
{
    public static class HealthCheckRegistration
    {
        public static IServiceCollection AddHealthCheckServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHealthChecks()
              .AddUrlGroup(new Uri(configuration.GetValue<string>("ExternalApi:httpbin") ?? string.Empty),
                   "httpbin", HealthStatus.Degraded, tags: new[] { "ready" }, timeout: new TimeSpan(0, 0, 15))
              .AddCheck("Mongo Db Health Check",new MongoHealthCheck(configuration), tags: new[] { "ready" });



            //services.AddHealthChecksUI(opt =>
            //{
            //    opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
            //    opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
            //    opt.SetApiMaxActiveRequests(1); //api requests concurrency    
            //    opt.AddHealthCheckEndpoint("default api", "/health"); //map health check api    
            //})
            //       .AddInMemoryStorage();

            //healthchecks-ui
            services.AddHealthChecksUI()
                   .AddInMemoryStorage();
            return services;
        }


    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.RateLimiting;

namespace CodeChallenge.Utility.ServiceRegisteration
{
    public static class RateLimiServiceRegisteration
    {
        public static IServiceCollection AddRateLimitServices(this IServiceCollection services)
        {
            services.AddRateLimiter(_ => _
                .AddFixedWindowLimiter(policyName: "fixed", options =>
                {
                    options.PermitLimit = 3;
                    options.Window = TimeSpan.FromSeconds(5);
                    options.QueueProcessingOrder = QueueProcessingOrder.NewestFirst;
                    options.QueueLimit = 0;
                }));

            return services;
        }
    }
}

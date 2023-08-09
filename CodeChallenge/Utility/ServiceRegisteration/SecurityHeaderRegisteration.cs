using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace CodeChallenge.Utility.ServiceRegisteration
{
    public static class SecurityHeaderRegisteration
    {
        public static IServiceCollection AddSecurityHeader(this IServiceCollection services)
        {
            services.AddAntiforgery(x =>
            {
                x.SuppressXFrameOptionsHeader = true;
            });

            services.AddHsts(x =>
            {
                x.Preload = true;
                x.IncludeSubDomains = true;
                x.MaxAge = TimeSpan.FromDays(60);
                x.ExcludedHosts.Add("example.com");
                x.ExcludedHosts.Add("www.example.com");
            });

            services.AddHttpsRedirection(x =>
            {
                x.HttpsPort = 7047;
                x.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
            });

            return services;
        }
    }
}

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace CodeChallenge.Utility.ServiceRegisteration
{
    public static class HealthCheckAppMiddleWare
    {
        public static WebApplication AddHealthCheckAppMiddleWare(this WebApplication app)
        {


            app.MapHealthChecks("/health/ready",
            new HealthCheckOptions()
            {
                ResultStatusCodes = {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded]=StatusCodes.Status500InternalServerError,
                        [HealthStatus.Unhealthy]=StatusCodes.Status503ServiceUnavailable,
                },
                ResponseWriter = WriteHealthCheckReadyResponse,
                //ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                Predicate = (check) => check.Tags.Contains("ready"),
                AllowCachingResponses = false

            }); ;

            app.MapHealthChecks("/health/live",
            new HealthCheckOptions()
            {

                ResponseWriter = WriteHealthCheckLiveResponse,
                //ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                Predicate = (check) => !check.Tags.Contains("ready"),
                AllowCachingResponses = false

            }); ;
          

            app.MapHealthChecks("/health",
             new HealthCheckOptions()
             {
                 Predicate = _ => true,
                 ResponseWriter = UIResponseWriter.
                          WriteHealthCheckUIResponse

             });
          //  app.MapHealthChecksUI();
            Task WriteHealthCheckLiveResponse(HttpContext context, HealthReport report)
            {
                context.Response.ContentType = "application/json";

                var json = new JObject()
                                       {
                                           new JProperty("OveralStatus",report.Status.ToString()),
                                           new JProperty("TotalCheckDuration",report.TotalDuration.TotalSeconds.ToString("0:00:00")),
                                          };

                return context.Response.WriteAsync(json.ToString((Newtonsoft.Json.Formatting)Formatting.Indented));
            };


            Task WriteHealthCheckReadyResponse(HttpContext context, HealthReport report)
            {
                context.Response.ContentType = "application/json";
                var json = new JObject()
                                          {
            new JProperty("OveralStatus",report.Status.ToString()),
            new JProperty("TotalCheckDuration",report.TotalDuration.TotalSeconds.ToString("0:00:00")),
            new JProperty("DependencyHealthCheck",new JObject(report.Entries.Select(dicitem=>
            new  JProperty(dicitem.Key,new JObject(

            new JProperty("OveralStatus",dicitem.Value.Status.ToString()),
            new JProperty("TotalCheckDuration",report.TotalDuration.TotalSeconds.ToString("0:00:00"))))

           ))),};

                return context.Response.WriteAsync(json.ToString((Newtonsoft.Json.Formatting)Formatting.Indented));
            }

            return app;
        }

    }
}

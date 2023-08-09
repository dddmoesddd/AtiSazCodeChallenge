using CodeChallenge.Utility.Middlewars;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using Serilog.Sinks.Elasticsearch;
using Serilog;

namespace CodeChallenge.Utility.ServiceRegisteration
{
    public static class LogServiceRegisteration
    {
        public static IServiceCollection AddLogServicees(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<RequestResponseLoggerOption>().Bind(configuration.GetSection("RequestResponseLogger")).ValidateDataAnnotations();
            services.AddSingleton<IRequestResponseLogger, RequestResponseLogger>();
            services.AddScoped<IRequestResponseLogModelCreator, RequestResponseLogModelCreator>();
     
            ConfigureLogging();
            return services;
        }

        static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }


        static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}

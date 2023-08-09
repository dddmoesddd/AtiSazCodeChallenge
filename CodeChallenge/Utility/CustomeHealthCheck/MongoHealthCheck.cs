using CodeChallenge.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeChallenge.Utility.CustomeHealthCheck
{
    public class MongoHealthCheck : IHealthCheck
    {
        private IMongoDatabase _db { get; set; }
        public MongoClient _mongoClient { get; set; }

        public MongoHealthCheck(IConfiguration configuration)
        {

            _mongoClient = new MongoClient(configuration.GetValue<string>("ReadDatabaseSettings:ConnectionString"));
            _db = _mongoClient.GetDatabase(configuration.GetValue<string>("ReadDatabaseSettings:DatabaseName"));
         

        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            var healthCheckResultHealthy = await CheckMongoDBConnectionAsync();


            if (healthCheckResultHealthy)
            {
                return HealthCheckResult.Healthy("MongoDB health check success");
            }

            return HealthCheckResult.Unhealthy("MongoDB health check failure"); ;
        }

        private async Task<bool> CheckMongoDBConnectionAsync()
        {
            try
            {
                await _db.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
            }

            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}

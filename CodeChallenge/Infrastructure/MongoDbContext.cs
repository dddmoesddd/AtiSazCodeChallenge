using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CodeChallenge.Infrastructure
{
    public class MongoDbContext : IMongoDbContext
    {
        public IMongoDatabase Db { get; set; }
        private MongoClient MongoClient { get; set; }
        public IMongoCollection<TODODbModel> TODODBModel { get; }
        public IClientSessionHandle Session { get; set; }
        public MongoDbContext(IConfiguration configuration)
        {
  
            MongoClient = new MongoClient(configuration.GetValue<string>("ReadDatabaseSettings:ConnectionString"));
            Db = MongoClient.GetDatabase(configuration.GetValue<string>("ReadDatabaseSettings:DatabaseName"));
            TODODBModel = Db.GetCollection<TODODbModel>(nameof(TODODBModel));

        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Db.GetCollection<T>(name);
        }
    }
}

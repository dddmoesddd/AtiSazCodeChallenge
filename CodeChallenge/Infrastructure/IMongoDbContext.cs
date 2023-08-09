using MongoDB.Driver;

namespace CodeChallenge.Infrastructure
{
    public interface IMongoDbContext
    {
        IMongoDatabase Db { get; set; }
        IMongoCollection<T> GetCollection<T>(string name);
    }
}

using CodeChallenge.Utility;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodeChallenge.Infrastructure.Repositories
{
    public class MongoRepository : IMongoRepository
    {


        public IMongoClient MongoClient { get; }
        public IMongoCollection<TODODbModel> Collection { get; }

        public MongoRepository(IMongoDbContext context)
        {

            MongoClient = context.Db.Client;
            Collection = context.Db.GetCollection<TODODbModel>(nameof(TODODbModel));
        }

        public ValueTask<List<TODODbModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async ValueTask<RepositoryResult<TODODbModel>> AddAsync(TODODbModel entity, CancellationToken cancellationToken = default)
        {
            try
            {
                entity.id = new Random().Next();
                await Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
                //await Collection.FindAsync(Builders<TODODBModel>.Filter.Empty);
                return new RepositoryResult<TODODbModel>() { IsSuccess = true };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


    }
}

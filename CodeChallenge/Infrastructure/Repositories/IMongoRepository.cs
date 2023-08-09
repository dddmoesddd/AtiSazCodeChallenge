using CodeChallenge.Infrastructure;
using CodeChallenge.Utility;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodeChallenge.Infrastructure.Repositories
{
    public interface IMongoRepository
    {

        public ValueTask<List<TODODbModel>> GetAllAsync();

        public ValueTask<RepositoryResult<TODODbModel>> AddAsync(TODODbModel entity, CancellationToken cancellationToken = default);
    }
}

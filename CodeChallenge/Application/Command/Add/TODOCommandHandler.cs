using CodeChallenge.Infrastructure;
using CodeChallenge.Infrastructure.Repositories;
using CodeChallenge.Utility;
using CodeChallenge.Utility.Exceptions;
using CodeChallenge.Utility.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeChallenge.Application.Command.Add
{
    public class TODOCommandHandler : IRequestHandler<TODOCommand, Result>
    {

        private IMongoRepository _repo;
        public TODOCommandHandler(IMongoRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result> Handle(TODOCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = new TODODbModel();
                model.Priority = request.Priority;
                model.Description = request.Description;
                model.Title = request.Title;
                await _repo.AddAsync(model);
             
                return Task.FromResult(new Result()
                {
                    IsSucess = true,
                    ReturnValue = model,
                    Message= CodeChallengeMessages.AddDataSuccess,
                    StausCode=200
                    
                }).Result;

            }
            catch (System.Exception)
            {

                throw new AddDataException(CodeChallengeMessages.AddDataException);
            }
      
        }
    }
}

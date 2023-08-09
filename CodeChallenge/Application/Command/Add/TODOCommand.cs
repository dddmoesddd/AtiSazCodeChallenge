using CodeChallenge.Model;
using CodeChallenge.Utility;
using MediatR;

namespace CodeChallenge.Application.Command.Add
{
    public class TODOCommand : IRequest<Result>
    {
        public  string Title { get; set; }

        public string Description { get; set; }


        public PriorityEnum Priority { get; set; }
    }
}

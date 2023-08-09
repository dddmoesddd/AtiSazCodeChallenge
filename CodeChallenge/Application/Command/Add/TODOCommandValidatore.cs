using FluentValidation;
using System.Linq;

namespace CodeChallenge.Application.Command.Add
{
    public class TODOCommandValidatore : AbstractValidator<TODOCommand>
    {

        public TODOCommandValidatore()
        {

            RuleFor(p => p.Title).NotEmpty().NotNull().MaximumLength(100).
            Matches("[A-Z] || [a-z]").
            WithMessage("'{PropertyName}' must contain one or more capital letters."); ;

            //.Matches(" ").WithMessage("'{PropertyName}' must contain one or more digits.").WithMessage("");
            RuleFor(p => p.Title)
            .Must(exp => !exp.Any(char.IsDigit)).WithMessage("Name should not Contain any Numbers").NotEmpty().NotNull().MaximumLength(100);


            RuleFor(p => p.Description)

           .MaximumLength(500).WithMessage("wrererere");


        }



    }
}


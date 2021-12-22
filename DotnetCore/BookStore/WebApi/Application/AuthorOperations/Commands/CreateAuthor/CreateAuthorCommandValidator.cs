using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.model.Name).MinimumLength(3).MaximumLength(15).NotEmpty();
            RuleFor(command => command.model.Birthday).NotNull().LessThan(System.DateTime.Now);
        }
        
    }
}
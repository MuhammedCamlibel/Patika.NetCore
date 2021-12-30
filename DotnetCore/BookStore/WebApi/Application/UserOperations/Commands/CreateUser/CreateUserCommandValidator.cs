using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(2).MaximumLength(20).NotNull();
            RuleFor(command => command.Model.Surname).MinimumLength(2).MaximumLength(20).NotNull();
            RuleFor(command => command.Model.Email).EmailAddress().NotNull();
            RuleFor(command => command.Model.Password).NotNull().NotEmpty().MinimumLength(4).MaximumLength(30);
        }
    }
}
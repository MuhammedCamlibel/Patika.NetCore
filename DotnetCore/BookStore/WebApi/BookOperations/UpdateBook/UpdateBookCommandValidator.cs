using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).NotNull();
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.model.GenreId).GreaterThan(0);
            RuleFor(command => command.model.Title).NotEmpty().MinimumLength(4);
        }
    }
}
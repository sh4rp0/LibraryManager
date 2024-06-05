using FluentValidation;

namespace LibraryManager.Application.Books.Commands;

public class CreateCommandValidation : AbstractValidator<CreateCommand>
{
    public CreateCommandValidation()
    {
        RuleFor(x => x.Author).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
    }
}

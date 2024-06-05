using FluentValidation;
using LibraryManager.Application.Common.Interfaces.Services;

namespace LibraryManager.Application.BookBorrowing.Commands;

public class BorrowCommandValidation : AbstractValidator<BorrowCommand>
{
    public BorrowCommandValidation()
    {
        RuleFor(x => x.MaxDaysUntilReturn).GreaterThan(0);
    }
}

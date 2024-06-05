using MediatR;
using ErrorOr;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.BookBorrowing.Commands;

public record BorrowCommand(
    Guid UserId,
    int BookId,
    int MaxDaysUntilReturn) : IRequest<ErrorOr<Borrowing>>;

using ErrorOr;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.BookBorrowing.Commands;

public record ReturnCommand(
    int Id,
    bool IsReturned) : IRequest<ErrorOr<Borrowing>>;

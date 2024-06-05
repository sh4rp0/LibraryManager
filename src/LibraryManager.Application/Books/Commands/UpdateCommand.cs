using ErrorOr;
using MediatR;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Books.Commands;

public record UpdateCommand(
    int Id,
    string Title,
    string Description,
    string Author,
    string ISBN,
    int TotalCopies) : IRequest<ErrorOr<Book>>;

using ErrorOr;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.Books.Commands;

public record CreateCommand(
    string Title,
    string Description,
    string Author,
    string ISBN,
    int TotalCopies) : IRequest<ErrorOr<Book>>;

using MediatR;
using ErrorOr;

namespace LibraryManager.Application.Books.Commands;

public record DeleteCommand(
    int Id) : IRequest<ErrorOr<bool>>;

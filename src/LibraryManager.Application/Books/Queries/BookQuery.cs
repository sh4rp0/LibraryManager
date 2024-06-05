using MediatR;
using ErrorOr;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Books.Queries;

public record BookQuery(
    int Id) : IRequest<ErrorOr<Book>>;

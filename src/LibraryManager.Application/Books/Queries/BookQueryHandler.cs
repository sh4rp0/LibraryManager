using ErrorOr;
using MediatR;
using LibraryManager.Domain.Entities;
using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Domain.Common.Errors;

namespace LibraryManager.Application.Books.Queries;

public class BookQueryHandler : IRequestHandler<BookQuery, ErrorOr<Book>>
{
    private readonly IBookRepository _bookRepository;

    public BookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ErrorOr<Book>> Handle(BookQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // Check if book already exists
        var book = await _bookRepository.GetBookByIdAsync(request.Id);

        if (book is null)
        {
            return Errors.Book.BookNotFound;
        }

        return book;
    }
}

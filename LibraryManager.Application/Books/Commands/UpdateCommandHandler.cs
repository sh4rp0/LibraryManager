using LibraryManager.Domain.Common.Errors;
using LibraryManager.Domain.Entities;
using ErrorOr;
using MediatR;
using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Application.Common.Interfaces.Services;

namespace LibraryManager.Application.Books.Commands;

public class UpdateCommandHandler : IRequestHandler<UpdateCommand, ErrorOr<Book>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateCommandHandler(IBookRepository bookRepository, IDateTimeProvider dateTimeProvider)
    {
        _bookRepository = bookRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Book>> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        // Check if book already exists
        var book = await _bookRepository.GetBookByIdAsync(request.Id);

        if (book is null)
        {
            return Errors.Book.BookNotFound;
        }

        book.Title = request.Title;
        book.Description = request.Description;
        book.Author = request.Author;
        book.ISBN = request.ISBN;
        book.TotalCopies = request.TotalCopies;
        book.UpdatedDateTime = _dateTimeProvider.UtcNow;

        var result = await _bookRepository.UpdateBookAsync(book);

        return result;
    }
}

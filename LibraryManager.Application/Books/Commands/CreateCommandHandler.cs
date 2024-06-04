using ErrorOr;
using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Application.Common.Interfaces.Services;
using LibraryManager.Domain.Common.Errors;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.Books.Commands;

public class CreateCommandHandler : IRequestHandler<CreateCommand, ErrorOr<Book>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateCommandHandler(IBookRepository bookRepository, IDateTimeProvider dateTimeProvider)
    {
        _bookRepository = bookRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Book>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        // Check if book already exists
        if(_bookRepository.GetBookByTitleAndAuthor(request.Title, request.Author).Any())
        {
            return Errors.Book.DuplicateBook;
        }

        var newBook = new Book()
        {
            Title = request.Title,
            Description = request.Description,
            Author = request.Author,
            ISBN = request.ISBN,
            TotalCopies = request.TotalCopies,
            CreatedDateTime = _dateTimeProvider.UtcNow,
            UpdatedDateTime = _dateTimeProvider.UtcNow,
        };

        return _bookRepository.AddBook(newBook);
    }
}

using ErrorOr;
using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Application.Common.Interfaces.Services;
using LibraryManager.Domain.Common.Errors;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.BookBorrowing.Commands;

public class BorrowCommandHandler : IRequestHandler<BorrowCommand, ErrorOr<Borrowing>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBorrowingRepository _borrowingRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public BorrowCommandHandler(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        IBorrowingRepository borrowingRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _borrowingRepository = borrowingRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Borrowing>> Handle(BorrowCommand request, CancellationToken cancellationToken)
    {
        // Check user exists
        if ((await _userRepository.GetUserByIdAsync(request.UserId)) is null)
        {
            return Errors.User.UserNotFound;
        }

        // Check book exists
        if ((await _bookRepository.GetBookByIdAsync(request.BookId)) is null)
        {
            return Errors.Book.BookNotFound;
        }

        var newBorrowing = new Borrowing()
        {
            UserId = request.UserId,
            BookId = request.BookId,
            IsReturned = false,
            DueDate = _dateTimeProvider.UtcNow.Date.AddDays(request.MaxDaysUntilReturn),
            CreatedDateTime = _dateTimeProvider.UtcNow
        };

        return await _borrowingRepository.AddAsync(newBorrowing);
    }
}

using ErrorOr;
using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Application.Common.Interfaces.Services;
using LibraryManager.Domain.Common.Errors;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.BookBorrowing.Commands;

public class ReturnCommandHandler : IRequestHandler<ReturnCommand, ErrorOr<Borrowing>>
{
    private readonly IBorrowingRepository _borrowingRepository;

    public ReturnCommandHandler(IBorrowingRepository borrowingRepository)
    {
        _borrowingRepository = borrowingRepository;
    }

    public async Task<ErrorOr<Borrowing>> Handle(ReturnCommand request, CancellationToken cancellationToken)
    {
        var borrowing = await _borrowingRepository.GetByIdAsync(request.Id);

        // Check book exists
        if (borrowing is null)
        {
            return Errors.Borrowing.BorrowingNotFound;
        }

        borrowing.IsReturned = true;

        return await _borrowingRepository.UpdateAsync(borrowing);
    }
}

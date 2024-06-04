using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Common.Interfaces.Persistence;

public interface IBorrowingRepository
{
    public Task<Borrowing> CreateAsync(Borrowing borrowing);

    public Task<List<Borrowing>> GetAllNotReturnedForDueDateAsync(DateTime datetime);
}

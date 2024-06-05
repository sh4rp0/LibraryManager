using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Common.Interfaces.Persistence;

public interface IBorrowingRepository
{
    public Task<Borrowing> AddAsync(Borrowing borrowing);

    public Task<Borrowing> UpdateAsync(Borrowing borrowing);

    public Task<Borrowing?> GetByIdAsync(int id);

    public Task<List<Borrowing>> GetAllNotReturnedForDueDateAsync(DateTime datetime);
}

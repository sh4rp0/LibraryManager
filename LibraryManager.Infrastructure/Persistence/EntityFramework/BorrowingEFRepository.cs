using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Infrastructure.Persistence.EntityFramework
{
    public class BorrowingEFRepository : IBorrowingRepository
    {
        public async Task<Borrowing> CreateAsync(Borrowing borrowing)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Borrowing>> GetAllNotReturnedForDueDateAsync(DateTime datetime)
        {
            return new List<Borrowing>();
        }
    }
}

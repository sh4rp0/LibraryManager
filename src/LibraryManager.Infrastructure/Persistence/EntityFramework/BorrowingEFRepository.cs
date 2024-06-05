using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Domain.Common.Errors;
using System;

namespace LibraryManager.Infrastructure.Persistence.EntityFramework
{
    public class BorrowingEFRepository : IBorrowingRepository
    {
        private readonly LibraryContext _context;

        public BorrowingEFRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Borrowing> AddAsync(Borrowing borrowing)
        {
            _context.Borrowings.Add(borrowing);
            await _context.SaveChangesAsync();

            return borrowing;
        }

        public async Task<List<Borrowing>> GetAllNotReturnedForDueDateAsync(DateTime datetime)
        {
            return await _context.Borrowings.Where(x => !x.IsReturned && x.DueDate == datetime).ToListAsync();
        }

        public async Task<Borrowing?> GetByIdAsync(int id)
        {
            return await _context.Borrowings.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Borrowing> UpdateAsync(Borrowing borrowing)
        {
            _context.Borrowings.Update(borrowing);
            await _context.SaveChangesAsync();

            return borrowing;
        }
    }
}

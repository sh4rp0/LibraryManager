using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Persistence.EntityFramework
{
    public class BookEFRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookEFRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book is Book b)
            {
                _context.Books.Remove(b);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Book>> GetBookByTitleAndAuthorAsync(string title, string Author)
        {
            return await _context.Books.Where(x => x.Title == title && x.Author == Author).ToListAsync();
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return book;
        }
    }
}

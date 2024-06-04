using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Common.Interfaces.Persistence
{
    public interface IBookRepository
    {
        public Task<Book> AddBookAsync(Book book);

        public Task<Book?> GetBookByIdAsync(int id);

        public Task<Book> UpdateBookAsync(Book book);

        public Task DeleteBookAsync(int id);

        public Task<List<Book>> GetBookByTitleAndAuthorAsync(string title, string Author);
    }
}

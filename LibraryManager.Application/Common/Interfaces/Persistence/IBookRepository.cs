using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Common.Interfaces.Persistence
{
    public interface IBookRepository
    {
        public Book AddBook(Book book);

        public Book? GetBookById(int id);

        public Book UpdateBook(Book book);

        public void DeleteBook(int id);

        public List<Book> GetBookByTitleAndAuthor(string title, string Author);
    }
}

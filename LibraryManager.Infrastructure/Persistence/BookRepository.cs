using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Infrastructure.Persistence;

public class BookRepository : IBookRepository
{
    private static readonly List<Book> _books = new();
    private static int id = 1;

    public Book AddBook(Book book)
    {
        book.Id = id++;
        _books.Add(book);
        return book;
    }

    public void DeleteBook(int id)
    {
        var book = GetBookById(id);
        if(book != null)
        {
            _books.Remove(book);
        }
    }

    public Book? GetBookById(int id)
    {
        return _books.SingleOrDefault(x => x.Id == id);
    }

    public List<Book> GetBookByTitleAndAuthor(string title, string Author)
    {
        return _books.FindAll(x => x.Title == title && x.Author == Author);
    }

    public Book UpdateBook(Book book)
    {
        _books[book.Id-1] = book;
        return book;
    }
}

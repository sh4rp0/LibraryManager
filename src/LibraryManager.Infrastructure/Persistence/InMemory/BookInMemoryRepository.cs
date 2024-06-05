using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Infrastructure.Persistence.InMemory;

public class BookInMemoryRepository : IBookRepository
{
    private static readonly List<Book> _books = new();
    private static int id = 1;

    public async Task<Book> AddBookAsync(Book book)
    {
        await Task.CompletedTask;

        book.Id = id++;
        _books.Add(book);
        return book;
    }

    public async Task DeleteBookAsync(int id)
    {
        await Task.CompletedTask;

        var book = GetBookByIdAsync(id).Result;
        if (book != null)
        {
            _books.Remove(book);
        }
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        await Task.CompletedTask;

        return _books.SingleOrDefault(x => x.Id == id);
    }

    public async Task<List<Book>> GetBookByTitleAndAuthorAsync(string title, string Author)
    {
        await Task.CompletedTask;

        return _books.FindAll(x => x.Title == title && x.Author == Author);
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        await Task.CompletedTask;

        _books[book.Id - 1] = book;
        return book;
    }
}

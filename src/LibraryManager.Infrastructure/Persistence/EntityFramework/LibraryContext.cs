using Microsoft.EntityFrameworkCore;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Infrastructure.Persistence.EntityFramework;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Borrowing> Borrowings { get; set; } = null!;
}

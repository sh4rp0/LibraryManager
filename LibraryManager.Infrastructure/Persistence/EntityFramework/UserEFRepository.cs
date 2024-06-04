using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Persistence.EntityFramework;

public class UserEFRepository : IUserRepository
{
    private readonly LibraryContext _context;

    public UserEFRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(s => s.Email == email);
    }
}

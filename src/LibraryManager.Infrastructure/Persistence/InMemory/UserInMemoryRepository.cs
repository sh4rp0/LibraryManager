using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Infrastructure.Persistence.InMemory;

public class UserInMemoryRepository : IUserRepository
{
    private static readonly List<User> _users = new();

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        await Task.CompletedTask;
        return _users.SingleOrDefault(u => u.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        await Task.CompletedTask;
        return _users.SingleOrDefault(u => u.Id == id);
    }
}

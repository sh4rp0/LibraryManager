using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);

    Task<User?> GetUserByIdAsync(Guid id);

    Task AddAsync(User user);
}

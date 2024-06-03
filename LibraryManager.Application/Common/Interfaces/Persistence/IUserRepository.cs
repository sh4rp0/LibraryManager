using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);

    void Add(User user);
}

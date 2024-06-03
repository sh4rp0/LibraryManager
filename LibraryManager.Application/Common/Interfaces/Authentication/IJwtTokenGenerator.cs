using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}

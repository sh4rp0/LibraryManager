using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);

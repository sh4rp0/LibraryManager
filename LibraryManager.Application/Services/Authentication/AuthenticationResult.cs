using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Services;

public record AuthenticationResult(
    User User,
    string Token);
using ErrorOr;
using LibraryManager.Application.Authentication.Common;
using MediatR;

namespace LibraryManager.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;


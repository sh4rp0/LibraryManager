using ErrorOr;
using LibraryManager.Application.Authentication.Common;
using MediatR;

namespace LibraryManager.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;

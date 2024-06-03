using LibraryManager.Application.Authentication.Commands.Register;
using LibraryManager.Application.Authentication.Common;
using LibraryManager.Application.Authentication.Queries.Login;
using LibraryManager.Contracts.Authentication;
using Mapster;

namespace LibraryManager.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, scr => scr.User);
    }
}

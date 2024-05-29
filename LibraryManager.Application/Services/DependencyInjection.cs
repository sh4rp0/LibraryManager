using LibraryManager.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManager.Application.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        return services;
    }
}

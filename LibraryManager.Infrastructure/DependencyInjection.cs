using LibraryManager.Application.Common.Interfaces.Authentication;
using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Application.Common.Interfaces.Services;
using LibraryManager.Infrastructure.Authentication;
using LibraryManager.Infrastructure.Persistence;
using LibraryManager.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}

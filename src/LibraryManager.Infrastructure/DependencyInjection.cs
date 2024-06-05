using LibraryManager.Application.Common.Interfaces.Authentication;
using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Application.Common.Interfaces.Services;
using LibraryManager.Infrastructure.Authentication;
using LibraryManager.Infrastructure.Persistence.EntityFramework;
using LibraryManager.Infrastructure.Persistence.InMemory;
using LibraryManager.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddSingleton<IEmailSender, FakeEmailSender>();

        services.AddDbContext<LibraryContext>(opt =>
            opt.UseSqlite(configuration.GetConnectionString("LibraryManagerDb")));

        services.AddScoped<IUserRepository, UserEFRepository>();
        services.AddScoped<IBookRepository, BookEFRepository>();
        services.AddScoped<IBorrowingRepository, BorrowingEFRepository>();
        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
}

using LibraryManager.Api.Common.Mapping;

namespace LibraryManager.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddProblemDetails();
        
        services.AddMappings();

        return services;
    }
}

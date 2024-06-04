using FluentValidation;
using LibraryManager.Application.BorrowerNotification;
using LibraryManager.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Reflection;

namespace LibraryManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.Configure<NotificationSettings>(
            configuration.GetSection(NotificationSettings.SectionName));

        services.AddQuartz(q =>
        {
            // Register the job, loading the schedule from configuration
            q.AddJobAndTrigger<BorrowerNotificationJob>(configuration);
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}

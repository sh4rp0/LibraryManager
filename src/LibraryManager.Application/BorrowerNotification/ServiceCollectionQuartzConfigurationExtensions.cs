using Microsoft.Extensions.Configuration;
using Quartz;

namespace LibraryManager.Application.BorrowerNotification;

public static class ServiceCollectionQuartzConfigurationExtensions
{
    public static void AddJobAndTrigger<T>(
        this IServiceCollectionQuartzConfigurator quartz,
        IConfiguration config)
        where T : IJob
    {
        // Use the name of the IJob as the appsettings.json key
        string jobName = typeof(T).Name;

        // Try and load the schedule from configuration
        var configKey = $"Quartz:{jobName}";
        var cronSchedule = config[configKey];

        // https://productresources.collibra.com/docs/collibra/latest/Content/Cron/co_quartz-cron-syntax.htm
        // Example 
        // 0 0 * ? * * * = the top of every hour of every day.
        // */10 * * * * ? = every ten seconds.
        // 0 0 8-10 * * ? 2020 = 8, 9 and 10 o'clock of every day during the year 2020.
        // 0 0 6,19 ? * * = 6:00 AM and 7:00 PM every day.
        // 0 0/30 8-10 ? * * = 8:00, 8:30, 9:00, 9:30, 10:00 and 10:30 every day.
        // 0 0 9-17 * * MON-FRI = on the hour nine-to-five weekdays.
        // 0 0 0 25 12 ? = every Christmas Day at midnight, no matter what day of the week it is.
        // 0 15 10 ? * 6L 2022-2025 = 10:15 AM on every Friday of every month during the years 2022, 2023, 2024 and 2025.
        // 0 30 11 ? * 6#2 = 11:30 AM on the second Friday of every month.

        // every 5 seconds
        // "BorrowerNotificationJob": "0/5 * * * * ?"
        // every day at 6 am
        // "BorrowerNotificationJob": "0 0 6 * * ?"

        // Some minor validation
        if (string.IsNullOrEmpty(cronSchedule))
        {
            throw new Exception($"No Quartz.NET Cron schedule found for job in configuration at {configKey}");
        }

        // register the job as before
        var jobKey = new JobKey(jobName);
        quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

        quartz.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity(jobName + "-trigger")
            .WithCronSchedule(cronSchedule)); // use the schedule from configuration
    }
}

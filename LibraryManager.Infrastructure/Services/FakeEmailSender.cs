using LibraryManager.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace LibraryManager.Infrastructure.Services;

public class FakeEmailSender : IEmailSender
{
    private readonly ILogger<FakeEmailSender> _logger;

    public FakeEmailSender(ILogger<FakeEmailSender> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        _logger.LogInformation("Sending email to {EmailAddress} with {Subject} and {Body}", email, subject, body);
        await Task.CompletedTask;
    }
}

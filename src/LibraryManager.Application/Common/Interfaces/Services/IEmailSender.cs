namespace LibraryManager.Application.Common.Interfaces.Services;

public interface IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string body);
}

using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace LibraryManager.Application.BorrowerNotification;

[DisallowConcurrentExecution]
public class BorrowerNotificationJob : IJob
{
    private readonly IBorrowingRepository _borrowingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IEmailSender _emailSender;
    private readonly NotificationSettings _notificationSettings;
    private readonly ILogger<BorrowerNotificationJob> _logger;

    public BorrowerNotificationJob(
        IBorrowingRepository borrowingRepository,
        IUserRepository userRepository,
        IBookRepository bookRepository,
        IDateTimeProvider dateTimeProvider,
        IEmailSender emailSender,
        IOptions<NotificationSettings> notificationOptions,
        ILogger<BorrowerNotificationJob> logger)
    {
        _borrowingRepository = borrowingRepository;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _dateTimeProvider = dateTimeProvider;
        _emailSender = emailSender;
        _notificationSettings = notificationOptions.Value;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Job {Jobname} is running", nameof(BorrowerNotificationJob));

        var date = _dateTimeProvider.UtcNow.Date.AddDays(-_notificationSettings.DaysDueUntilNotify);
        var dueBorrowings = await _borrowingRepository.GetAllNotReturnedForDueDateAsync(date);

        foreach(var borrowing in dueBorrowings)
        {
            var user = await _userRepository.GetUserByIdAsync(borrowing.UserId);

            if(user == null)
            {
                continue;
            }

            var book = await _bookRepository.GetBookByIdAsync(borrowing.BookId);

            string title = "Your borrowed book is due to be returned";
            string body = $"Dear {user.FirstName}{Environment.NewLine}the book {book?.Title} you have borrowed" +
                $" at GoofyLibrary at {borrowing.CreatedDateTime.ToString()} is due to be returned soon.{Environment.NewLine}" +
                $"You have {_notificationSettings.DaysDueUntilNotify} days to return the book until penalty will be enacted.";

            await _emailSender.SendEmailAsync(user.Email, title, body);
        }
    }
}

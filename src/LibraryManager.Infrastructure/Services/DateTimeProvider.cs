using LibraryManager.Application.Common.Interfaces.Services;

namespace LibraryManager.Infrastructure.Services;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

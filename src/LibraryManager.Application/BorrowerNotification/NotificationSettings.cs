namespace LibraryManager.Application.BorrowerNotification;

public class NotificationSettings
{
    public const string SectionName = "NotificationSettings";

    public int DaysDueUntilNotify { get; set; }
}

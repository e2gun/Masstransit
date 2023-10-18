namespace MassShared.Events
{
    public interface INotificationCreated
    {
        DateTime NotificationDate { get; }
        string NotificationMessage { get; }
        NotificationType NotificationType { get; }
    }
}

using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Valid.Test.Domain.Notification
{
    public class NotificationContext : INotificationContext
    {
        public List<DomainNotification> Notifications { get; private set; }

        public NotificationContext() => Notifications = new List<DomainNotification>();

        public bool HasNotifications() => GetNotifications().Any();

        public List<DomainNotification> GetNotifications() => Notifications;

        public void AddNotification(string key, string message) =>
            Notifications.Add(new DomainNotification(key, message));

        public void AddNotification(DomainNotification notification) =>
            Notifications.Add(notification);

        public void AddNotifications(IEnumerable<DomainNotification> notifications) =>
            Notifications.AddRange(notifications);

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                AddNotification("Erro", error.ErrorMessage);
        }
    }
}

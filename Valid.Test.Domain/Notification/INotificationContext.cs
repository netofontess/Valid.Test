using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Valid.Test.Domain.Notification
{
    public interface INotificationContext
    {
        void AddNotification(string key, string message);
        void AddNotification(DomainNotification notification);
        void AddNotifications(IEnumerable<DomainNotification> notifications);
        void AddNotifications(ValidationResult validationResult);
        List<DomainNotification> GetNotifications();
        bool HasNotifications();
    }
}

namespace Valid.Test.Domain.Notification
{
    public class DomainNotification
    {
        public string Key { get; private set; }
        public string Message { get; }

        public DomainNotification(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}

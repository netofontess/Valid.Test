using Valid.Test.Domain.Notification;

namespace Valid.Test.Domain.Response
{
    public interface IResponse
    {
        void AddValidations(List<DomainNotification> erros);
        bool IsSuccess();
        void SetMessage(string description);
        void SetStackTrace(string stackTrace);
        Response SetResult(object obj);
        object GetResult();
    }
}

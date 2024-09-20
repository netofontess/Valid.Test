using System.Text.Json.Serialization;
using Valid.Test.Domain.Notification;

namespace Valid.Test.Domain.Response
{
    public class Response : IResponse
    {
        public Response()
        {
            Validations = new List<DomainNotification>();
        }

        public void SetMessage(string description)
        {
            Message = description;
            IsSuccess();
        }

        public void SetStackTrace(string stackTrace)
        {
            StackTrace = stackTrace;
        }

        public void AddValidations(List<DomainNotification> erros)
        {
            Validations = erros;
            IsSuccess();
        }

        public bool IsSuccess()
        {
            Success = Message == null && !Validations.Any();
            return Success;
        }

        public Response SetResult(object obj)
        {
            Result = obj;
            IsSuccess();
            return this;
        }

        public object GetResult()
        {
            return Result;
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StackTrace { get; set; }

        public List<DomainNotification> Validations { get; set; } = new List<DomainNotification>();

        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Result { get; set; }
    }
}

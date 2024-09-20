using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Valid.Test.Domain.Notification;
using Valid.Test.Domain.Response;

namespace Valid.Test.Api.Filters
{
    public class NotificationFilter(INotificationContext notificationContext,
        ILogger<NotificationFilter> logger,
        IResponse response) : IActionFilter
    {
        private readonly INotificationContext _notificationContext = notificationContext;
        private readonly ILogger _logger = logger;
        private readonly IResponse _response = response;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is null)
            {
                if (_notificationContext.HasNotifications())
                {
                    var message = "Erro na requisição. Verifique as mensagens.";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.HttpContext.Response.ContentType = "application/json";

                    var notifications = _notificationContext.GetNotifications();

                    _logger.LogWarning(message);
                    foreach (var item in notifications)
                    {
                        _logger.LogWarning(item.Message);
                    }

                    _response.SetMessage(message);
                    _response.AddValidations(notifications);

                    context.Result = new ObjectResult(_response);
                }
                else
                {
                    var result = (ObjectResult)context.Result;
                    _response.SetResult(result.Value!);
                    context.Result = new ObjectResult(_response);
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}

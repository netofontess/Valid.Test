using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Authentication;
using Valid.Test.Domain.Response;

namespace Valid.Test.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        private readonly IResponse Response;

        public ExceptionFilter(ILogger<ExceptionFilter> logger,
                               IResponse response)
        {
            _logger = logger;
            Response = response;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is InvalidCredentialException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                Response.SetMessage(exception.Message);
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.SetMessage(exception.Message);
            }
            context.HttpContext.Response.ContentType = "application/json";
            context.ExceptionHandled = true;

            Response.SetStackTrace(exception.StackTrace!);
            context.Result = new ObjectResult(Response);
            _logger.LogError(exception.ToString());
        }
    }
}

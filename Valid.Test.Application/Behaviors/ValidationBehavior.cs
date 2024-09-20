using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Valid.Test.Domain.Notification;

namespace Valid.Test.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    {
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly INotificationContext _notificationContext;

        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger,
            INotificationContext notificationContext,
            IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public Task<TResponse?> Handle(TRequest request, RequestHandlerDelegate<TResponse?> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("-- Validating command {Command}", request.GetType().Name);

            var failures = _validators
                 .Select(v => v.Validate(request))
                 .SelectMany(x => x.Errors)
                 .Where(f => f != null)
                 .ToList();

            return failures.Any() ? Notify(failures) : next();
        }

        private Task<TResponse?> Notify(IEnumerable<ValidationFailure> failures)
        {
            var result = default(TResponse);

            foreach (var failure in failures)
                _notificationContext.AddNotification(failure.PropertyName, failure.ErrorMessage);

            return Task.FromResult(result);
        }
    }
}

using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Valid.Test.Application.Amqp.MessageObjects;
using Valid.Test.Application.Services;
using Valid.Test.Application.Services.Interfaces;

namespace Valid.Test.Application.Amqp.Consumer
{
    public class GravarProtocoloConsumer(IProtocoloService protocoloService, ILogger<ProtocoloService> logger) : IConsumer<GravarProtocoloMessage>
    {
        private readonly IProtocoloService _protocoloService = protocoloService;
        private readonly ILogger<ProtocoloService> _logger = logger;

        public async Task Consume(ConsumeContext<GravarProtocoloMessage> context)
        {
            _logger.LogInformation("Consumer protocolo: {Message}", context.Message);
            await _protocoloService.GravarProtocoloAsync(context.Message);
        }
    }
}

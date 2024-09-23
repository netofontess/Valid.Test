using MassTransit;
using Microsoft.Extensions.Logging;
using Valid.Test.Application.Amqp.MessageObjects;

namespace Valid.Test.Application.Amqp.Publisher
{
    public class PublisherService(IPublishEndpoint publishEndpoint, ILogger<PublisherService> logger) : IPublisherService
    {
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly ILogger<PublisherService> _logger = logger;

        public async Task PublicarProtocoloAsync(GravarProtocoloMessage gravarProtocoloCommand)
        {
            _logger.LogInformation("Publicando protocolo: {ProtocoloDto}", gravarProtocoloCommand);
            await _publishEndpoint.Publish(gravarProtocoloCommand);
        }
    }
}

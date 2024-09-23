using Valid.Test.Application.Amqp.MessageObjects;

namespace Valid.Test.Application.Amqp.Publisher
{
    public interface IPublisherService
    {
        Task PublicarProtocoloAsync(GravarProtocoloMessage gravarProtocoloCommand);
    }
}

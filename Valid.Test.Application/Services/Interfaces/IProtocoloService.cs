using Valid.Test.Application.Amqp.MessageObjects;

namespace Valid.Test.Application.Services.Interfaces
{
    public interface IProtocoloService
    {
        Task GravarProtocoloAsync(GravarProtocoloMessage gravarProtocoloMessage);
    }
}

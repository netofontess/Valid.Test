using MediatR;
using Valid.Test.Domain.Helpers;
using Valid.Test.Domain.Models;

namespace Valid.Test.Application.UseCases.ProtocoloCase.Get
{
    public class ConsultaProtocoloQuery(FiltroProtocolo filtro) : IRequest<IEnumerable<Protocolo>>
    {
        public FiltroProtocolo Filtro { get; set; } = filtro;
    }
}

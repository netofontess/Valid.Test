using MediatR;
using Valid.Test.Domain.Models;
using Valid.Test.Repository.Repositories.Interfaces;

namespace Valid.Test.Application.UseCases.ProtocoloCase.Get
{
    public class ConsultaProtocoloHandler(IProtocoloRepository protocoloRepository) : IRequestHandler<ConsultaProtocoloQuery, IEnumerable<Protocolo>>
    {
        private readonly IProtocoloRepository _protocoloRepository = protocoloRepository;

        public Task<IEnumerable<Protocolo>> Handle(ConsultaProtocoloQuery request, CancellationToken cancellationToken)
        {
            var filtro = request.Filtro;

            var listaProtocolos = _protocoloRepository.ConsultaComFiltro(filtro);

            return listaProtocolos;
        }
    }
}

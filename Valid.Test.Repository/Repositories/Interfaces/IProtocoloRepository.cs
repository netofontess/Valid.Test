using Valid.Test.Domain.Helpers;
using Valid.Test.Domain.Models;

namespace Valid.Test.Repository.Repositories.Interfaces
{
    public interface IProtocoloRepository : IRepository<Protocolo>
    {
        Task<Protocolo?> ConsultaPorNumeroProtocoloAsync(string? numeroProtocolo);
        Task<Protocolo?> ConsultaPorCpfRgEhNumeroViaAsync(string? cpf, string? rg, int numeroVia);
        Task<IEnumerable<Protocolo>> ConsultaComFiltro(FiltroProtocolo filtro);
    }
}

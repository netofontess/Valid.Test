using Valid.Test.Domain.Models;

namespace Valid.Test.Repository.Repositories.Interfaces
{
    public interface IProtocoloRepository : IRepository<Protocolo>
    {
        Task<Protocolo?> FindByNumeroProtocoloAsync(string? numeroProtocolo);
        Task<Protocolo?> FindByCpfRgAndNumeroViaAsync(string? cpf, string? rg, int numeroVia);
    }
}

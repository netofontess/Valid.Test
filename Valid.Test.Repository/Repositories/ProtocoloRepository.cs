using Microsoft.EntityFrameworkCore;
using Valid.Test.Domain.Models;
using Valid.Test.Repository.Contexts;
using Valid.Test.Repository.Repositories.Interfaces;

namespace Valid.Test.Repository.Repositories
{
    public class ProtocoloRepository : Repository<Protocolo>, IProtocoloRepository
    {
        private readonly ValidContext _context;
        public ProtocoloRepository(ValidContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Protocolo?> FindByNumeroProtocoloAsync(string? numeroProtocolo)
        {
            return await _context.Protocolo.FirstOrDefaultAsync(p => p.NumeroProtocolo == numeroProtocolo);
        }

        public async Task<Protocolo?> FindByCpfRgAndNumeroViaAsync(string? cpf, string? rg, int numeroVia)
        {
            return await _context.Protocolo
                .FirstOrDefaultAsync(p => (p.Cpf == cpf || p.Rg == rg) && p.NumeroVia == numeroVia);
        }
    }
}

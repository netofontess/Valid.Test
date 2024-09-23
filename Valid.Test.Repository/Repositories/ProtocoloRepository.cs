using Microsoft.EntityFrameworkCore;
using Valid.Test.Domain.Helpers;
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

        public async Task<Protocolo?> ConsultaPorNumeroProtocoloAsync(string? numeroProtocolo)
        {
            return await _context.Protocolo.FirstOrDefaultAsync(p => p.NumeroProtocolo == numeroProtocolo);
        }

        public async Task<Protocolo?> ConsultaPorCpfRgEhNumeroViaAsync(string? cpf, string? rg, int numeroVia)
        {
            return await _context.Protocolo
                .FirstOrDefaultAsync(p => (p.Cpf == cpf || p.Rg == rg) && p.NumeroVia == numeroVia);
        }

        public async Task<IEnumerable<Protocolo>> ConsultaComFiltro(FiltroProtocolo filtro)
        {
            var query = _context.Protocolo.AsQueryable();

            if (!string.IsNullOrEmpty(filtro.NumeroProtocolo))
            {
                query = query.Where(p => p.NumeroProtocolo == filtro.NumeroProtocolo);
            }

            if (!string.IsNullOrEmpty(filtro.Cpf))
            {
                query = query.Where(p => p.Cpf == filtro.Cpf);
            }

            if (!string.IsNullOrEmpty(filtro.Rg))
            {
                query = query.Where(p => p.Rg == filtro.Rg);
            }

            var result = await query.ToListAsync();

            return result;
        }
    }
}

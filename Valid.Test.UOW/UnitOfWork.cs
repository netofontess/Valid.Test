using Valid.Test.Repository.Contexts;
using Valid.Test.Repository.Repositories.Interfaces;

namespace Valid.Test.UOW
{
    public class UnitOfWork(ValidContext context,
                      IProtocoloRepository protocoloRepository) : IUnitOfWork
    {
        private readonly ValidContext _context = context;

        public async Task Commit() => await _context.CommitAsync();

        public IProtocoloRepository ProtocoloRepository { get; } = protocoloRepository;
    }
}

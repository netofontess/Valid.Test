using Valid.Test.Repository.Repositories.Interfaces;

namespace Valid.Test.UOW
{
    public interface IUnitOfWork
    {
        Task Commit();
        IProtocoloRepository ProtocoloRepository { get; }
    }
}

namespace Valid.Test.Repository.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Insert(T Entity);

        Task Insert(IEnumerable<T> Entity);

        void Update(T Entity);

        void Update(IEnumerable<T> Entity);

        Task<T> GetFirstOrDefault(Func<T, bool> predicate);

        Task<IEnumerable<T>> Get(Func<T, bool> predicate);

        IQueryable<T> GetQueryable();

        void Delete(T Entity);

        void Delete(IEnumerable<T> Entity);
    }
}

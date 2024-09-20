using Microsoft.EntityFrameworkCore;
using Valid.Test.Repository.Repositories.Interfaces;

namespace Valid.Test.Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> Get(Func<T, bool> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).AsQueryable()
                .ToListAsync();
        }

        public async Task<T> GetFirstOrDefault(Func<T, bool> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).AsQueryable()
                .FirstOrDefaultAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task Insert(T Entity)
        {
            await _dbSet.AddAsync(Entity);
        }

        public async Task Insert(IEnumerable<T> Entity)
        {
            await _dbSet.AddRangeAsync(Entity);
        }

        public void Update(T Entity)
        {
            _dbSet.Update(Entity);
        }

        public void Update(IEnumerable<T> Entity)
        {
            _dbSet.UpdateRange(Entity);
        }

        public void Delete(T Entity)
        {
            _dbSet.Remove(Entity);
        }

        public void Delete(IEnumerable<T> Entity)
        {
            _dbSet.RemoveRange(Entity);
        }
    }
}

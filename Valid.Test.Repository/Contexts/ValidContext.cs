using Microsoft.EntityFrameworkCore;
using Valid.Test.Domain.Models;

namespace Valid.Test.Repository.Contexts
{
    public class ValidContext(DbContextOptions<ValidContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ValidContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Protocolo> Protocolo { get; set; }

        public async Task<bool> CommitAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}

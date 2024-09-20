namespace Valid.Test.Domain.Models.Base
{
    public abstract class Entity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}

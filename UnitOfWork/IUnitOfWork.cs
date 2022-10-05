using CustomerManagement.Data;

namespace CustomerManagement.UnitOfWork
{
    public interface IUnitOfWork<out TContext>
        where TContext : DBEntities, new()
    {
        TContext Context { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}

using Data.Repositories.Interfaces;

namespace Data
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; }
        public IOrderRepository Order { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
        void Dispose();
        Task<int> SaveChangesAsync();
    }
}

using Data.Repositories.Interfaces;
using Domain.Entities;

namespace Data.Repositories.Implementations
{
    public class OrderTransactionRepository : Repository<OrderTransaction>, IOrderTransactionRepository
    {
        public OrderTransactionRepository(SuaMe88Context context) : base(context)
        {
        }
    }
}

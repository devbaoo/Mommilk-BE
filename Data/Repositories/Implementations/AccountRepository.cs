using Data.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class AccountRepository : Repository<Customer>, IAccountRepository
    {
        public AccountRepository(SuaMe88Context context) : base(context)
        {
        }

        public async Task<Customer> GetCustomerByEmailAndPasswordAsync(string email, string password)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
    }
}

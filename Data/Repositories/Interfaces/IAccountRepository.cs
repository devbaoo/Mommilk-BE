using Domain.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IAccountRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByEmailAndPasswordAsync(string email, string password);
    }
}
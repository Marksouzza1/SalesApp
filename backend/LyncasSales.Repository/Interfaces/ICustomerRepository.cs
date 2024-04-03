using LyncasSales.Domain.Entities;
using LyncasSales.Repository.Filter;

namespace LyncasSales.Repository.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<List<Customer>> GetCustomerAsync();
        Customer GetById(int id);
        Task<int> GetTotalCustomerCountAsync();
        Task<List<Customer>> GetCustomersAsync(CustomerFilterParams filterParams);
    }
}

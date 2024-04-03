
using LyncasSales.Domain.Entities;
using LyncasSales.Repository.Filter;
using LyncasSales.Repository.Interfaces;

namespace LyncasSales.Services.Filter
{
    public class CustomerFilter
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerFilter(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> FilterCustomer(CustomerFilterParams customerFilter)
        {
            return await _customerRepository.GetCustomersAsync(customerFilter);
        }
    }
}

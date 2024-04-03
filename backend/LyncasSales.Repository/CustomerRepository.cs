using LyncasSales.Domain.Data;
using LyncasSales.Domain.Entities;
using LyncasSales.Repository.Filter;
using LyncasSales.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LyncasSales.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomerAsync()
        {
            var customer = await _context.Customers.Where(x => x.IsDeleted == false)
                .ToListAsync();
            return customer;
        }

        public Customer GetById(int id)
        {
            var customer = _context.Customers.Where(x => x.CustomerId == id && x.IsDeleted == false).FirstOrDefault();

            return customer;
        }


        public async Task<List<Customer>> GetCustomersAsync(CustomerFilterParams filterParams)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.SearchTerm))
                query = query.Where(c => c.Name.Contains(filterParams.SearchTerm) ||
                                   c.Email.Contains(filterParams.SearchTerm) ||
                                   c.PhoneNumber.Contains(filterParams.SearchTerm) ||
                                   c.Cpf.Contains(filterParams.SearchTerm));

            var customers = await query
                .Skip((filterParams.PageNumber - 1) * filterParams.PageQuantity)
                .Take(filterParams.PageQuantity)
                .ToListAsync();

            return customers;
        }

        public async Task<int> GetTotalCustomerCountAsync()
        {
            return await _context.Customers.CountAsync();
        }
    }
}

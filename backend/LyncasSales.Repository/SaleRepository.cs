using LyncasSales.Domain.Data;
using LyncasSales.Domain.Entities;
using LyncasSales.Repository.Filter;
using LyncasSales.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LyncasSales.Repository
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

   

        public async Task<int> GetTotalSalesCountAsync()
        {
            return await _context.Sale.CountAsync();
        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            var sale = await _context.Sale
                .Include(s => s.Customer)
                .Include(s => s.Items) 
                .FirstOrDefaultAsync(s => s.Id == id);

            return sale;
        }

        public async Task<List<Sale>> SearchAsync(SaleFilterParams filterParams)
        {
            var query = _context.Sale
                .Include(s => s.Customer)
                .Include(s => s.Items)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterParams.SearchTerm))
            {
                query = query.Where(s =>
                    s.Id.ToString().Contains(filterParams.SearchTerm) ||
                    s.Customer.Name.Contains(filterParams.SearchTerm) ||
                    s.BillingDate.ToString().Contains(filterParams.SearchTerm) ||
                    s.SaleDate.ToString().Contains(filterParams.SearchTerm) ||
                    s.TotalAmount.ToString().Contains(filterParams.SearchTerm));
            }

            var sales = await query
                .Skip((filterParams.PageNumber - 1) * filterParams.PageQuantity)
                .Take(filterParams.PageQuantity)
                .ToListAsync();

            return sales;
        }


        public async Task<bool> HasSalesByCustomerIdAsync(int customerId)
        {
            return await _context.Sale.AnyAsync(s => s.CustomerId == customerId);
        }

      
    }
}

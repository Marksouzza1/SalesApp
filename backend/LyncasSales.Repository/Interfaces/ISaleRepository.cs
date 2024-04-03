using LyncasSales.Domain.Entities;
using LyncasSales.Repository.Filter;
namespace LyncasSales.Repository.Interfaces
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        Task<Sale> GetByIdAsync(int id);
        Task<bool> HasSalesByCustomerIdAsync(int customerId);
        Task<int> GetTotalSalesCountAsync();
        Task<List<Sale>> SearchAsync(SaleFilterParams filterParams);


    }
}

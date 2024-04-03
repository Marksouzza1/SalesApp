

using LyncasSales.Domain.Entities;
using LyncasSales.Repository.Interfaces;
using LyncasSales.Repository.Filter;

namespace LyncasSales.Services.Filter
{
    public class SaleFilter
    {
        private readonly ISaleRepository _saleRepository;

        public SaleFilter(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<List<Sale>> FilterSales(SaleFilterParams filterParams)
        {
            return await _saleRepository.SearchAsync(filterParams);
        }
    }
}

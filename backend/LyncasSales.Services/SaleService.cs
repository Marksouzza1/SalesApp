using AutoMapper;
using LyncasSales.Domain.DTO;
using LyncasSales.Domain.Entities;
using LyncasSales.Domain.ViewModel;
using LyncasSales.Repository.Filter;
using LyncasSales.Repository.Interfaces;

namespace LyncasSales.Services
{
    public class SaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleResponseDTO> SearchAsync(SaleFilterParams filterParams)
        {
            var totalSales = await _saleRepository.GetTotalSalesCountAsync();
            var sales = await _saleRepository.SearchAsync(filterParams);
            return await GetSaleResponseAsync(totalSales, filterParams.PageQuantity, sales);
        }



        public async Task<Sale> GetSaleByIdAsync(int id)
        {
                var sale = await _saleRepository.GetByIdAsync(id);
                var saleViewModel = _mapper.Map<Sale>(sale);
                return saleViewModel;
        }
        public  async Task<Sale> CreateSale(CreateSaleDTO createSaleDTO)
        {
            try
            {
               var createSale = _mapper.Map<Sale>(createSaleDTO);
                await _saleRepository.Create(createSale);
                await _saleRepository.SaveChangesAsync();
                return createSale;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating sale: {ex.Message}");
                throw;
            }
        }
        public async Task<Sale> UpdateSale(int id, UpdateSaleDTO updateSaleDTO)
        {
            var sale = await _saleRepository.GetByIdAsync(id);

            var updateSale = _mapper.Map(updateSaleDTO, sale);

            await _saleRepository.Update(updateSale);
            await _saleRepository.SaveChangesAsync();

            return updateSale;
        }
        public async Task<bool> DeleteSale(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            var deleteSale = _mapper.Map<Sale>(sale);
            await _saleRepository.Delete(deleteSale);
            return await _saleRepository.SaveChangesAsync();
        }

        private async Task<SaleResponseDTO> GetSaleResponseAsync(int totalSales, int pageQuantity, List<Sale> sales)
        {
            var totalPages = (int)Math.Ceiling((double)totalSales / pageQuantity);
            return new SaleResponseDTO
            {
                TotalPages = totalPages,
                Sales = sales
            };
        }



    }
}

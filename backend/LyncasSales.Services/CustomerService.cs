using AutoMapper;
using LyncasSales.Domain.DTO;
using LyncasSales.Domain.Entities;
using LyncasSales.Domain.ViewModel;
using LyncasSales.Repository.Filter;
using LyncasSales.Repository.Interfaces;

namespace LyncasSales.Services
{
    public class CustomerService
    {   
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;

        public CustomerService(ICustomerRepository customerRepository, ISaleRepository saleRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        public  CustomerViewModel GetById(int id)
        {
            var customer = _customerRepository.GetById(id);
            var getCustomer = _mapper.Map<CustomerViewModel>(customer);
            return getCustomer;
        }
        public async Task<List<CustomerViewModel>> GetCustomersAsync(CustomerFilterParams filterParams)
        {
            var customers = await _customerRepository.GetCustomersAsync(filterParams);
            var mappedCustomers = _mapper.Map<List<CustomerViewModel>>(customers);
            return mappedCustomers;
        }

        public async Task<CustomerResponseDTO> SearchAsync(CustomerFilterParams filterParams)
        {
            var totalSales = await _customerRepository.GetTotalCustomerCountAsync();
            var sales = await _customerRepository.GetCustomersAsync(filterParams);
            return await GetCustomerResponseAsync(totalSales, filterParams.PageQuantity, sales);
        }

        public async Task<List<CustomerViewModel>> GetCustomerAsync()
        {
            var customer = await _customerRepository.GetCustomerAsync();
            var mapCustomer = _mapper.Map<List<CustomerViewModel>>(customer);
            return mapCustomer;
        }

        private async Task<CustomerResponseDTO> GetCustomerResponseAsync(int totalCustomer, int pageQuantity, List<Customer> customers)
        {
            var totalPages = (int)Math.Ceiling((double)totalCustomer / pageQuantity);
            return new CustomerResponseDTO
            {
                TotalPages = totalPages,
                customers = customers
            };
        }

     


            public async Task<(bool IsDeleted, string Message)> DeleteCustomerAsync(int id)
        {
            var customer = _customerRepository.GetById(id) ?? throw new ArgumentException("Cliente não encontrado.");

            var hasSales = await _saleRepository.HasSalesByCustomerIdAsync(id);
            if (hasSales)
            {
                customer.IsDeleted = true;
                await _customerRepository.Update(customer);
                await _customerRepository.SaveChangesAsync();
                return (false,"user now is inative") ;
            }
            else
            {
                await  _customerRepository.Delete(customer);
                await _customerRepository.SaveChangesAsync();
                return (true, "user deleted");
            }
             
   
        
        }
    }
}
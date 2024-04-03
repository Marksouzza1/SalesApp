using LyncasSales.Domain.DTO;
using LyncasSales.Domain.Entities;
using LyncasSales.Repository.Filter;
using LyncasSales.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LyncasSales.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1")]
    public class CustomersController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly BaseService<Customer, CreateCustomerDTO> _baseService;

        public CustomersController(CustomerService customerService, BaseService<Customer, CreateCustomerDTO> baseService)
        {
            _customerService = customerService;
            _baseService = baseService;
        }

        [HttpGet("customer")]
        public async Task<IActionResult> Get()
        {
            var customers = await _customerService.GetCustomerAsync();

            return Ok(customers);
        }
        [HttpPost("customers")]
        public async Task<IActionResult> GetCustomers([FromBody] CustomerFilterParams filterParams)
        {
            var customersResponse = await _customerService.SearchAsync(filterParams);
            Response.Headers.Add("X-Page-Size", filterParams.PageQuantity.ToString());
            Response.Headers.Add("X-Total-Pages", customersResponse.TotalPages.ToString());

            return Ok(customersResponse.customers);
        }



        [HttpGet("customer/{id}")]
        public  IActionResult GetById(int id)
        {
            var customer =  _customerService.GetById(id);
            return Ok(customer);
        }
        [HttpPost]
        [Route("customer")]
        public async Task<IActionResult> CreateAsync(CreateCustomerDTO createCustomer)
        {

            var newCustomer = await _baseService.CreateAsync(createCustomer);
            

            return Ok(newCustomer);
        }



        [HttpPut("customer/{id}")]
        public async Task<IActionResult> PutAsync(int id, CreateCustomerDTO customer)
        {

            var updateCustomer = await _baseService.UpdateAsync(id, customer);

            return Ok(updateCustomer);

        }

        [HttpDelete("customer/{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("invalid id");
            }

            var deletecustomer = await _customerService.DeleteCustomerAsync(id);
            
            if (deletecustomer.IsDeleted)
            {
                return Ok(deletecustomer);
            }
            else
            {
                return Ok(deletecustomer.Message);
            }

        }

    }

}
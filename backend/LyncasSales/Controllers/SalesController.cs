using LyncasSales.Domain.DTO;
using LyncasSales.Domain.Entities;
using LyncasSales.Repository.Filter;
using LyncasSales.Services;
using Microsoft.AspNetCore.Mvc;

namespace LyncasSales.Controllers
{
    [Route("v1")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SaleService _saleService;
        private readonly BaseService<Sale, CreateSaleDTO> _baseService;

        public SalesController(SaleService saleService, BaseService<Sale, CreateSaleDTO> baseService)
        {
            _saleService = saleService;
            _baseService = baseService;
        }

        [HttpPost("getall")]
        public async Task<IActionResult> SearchSales([FromBody] SaleFilterParams filterParams)
        {
            var salesResponse = await _saleService.SearchAsync(filterParams);

            Response.Headers.Add("X-Page-Size", filterParams.PageQuantity.ToString());
            Response.Headers.Add("X-Total-Pages", salesResponse.TotalPages.ToString());

            return Ok(salesResponse.Sales);
        }

    


        [HttpGet("sale/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }


        [HttpPost("sale")]
        public async Task<ActionResult<Sale>> CreateSale([FromBody] CreateSaleDTO createSale)
        {
            try
            {
                var newSale = await _baseService.CreateAsync(createSale);
                return Ok(newSale);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("sale/{id}")]
        public async Task<IActionResult> PutAsync(int id, CreateSaleDTO sale)
        {

            var updateSale = await _baseService.UpdateAsync(id, sale);
            return Ok(updateSale);
        }

        [HttpDelete("sale/{id}")]
        public async Task <IActionResult> DeleteSale(int id)
        {
            if (id == 0)
            {
                return BadRequest();

            }
            var delete = await _saleService.DeleteSale(id);
            return Ok(delete);
        }
    }
}


using System.ComponentModel.DataAnnotations;

namespace LyncasSales.Domain.DTO
{
    public class CreateSaleDTO
    {
        public int CustomerId { get; set; }
        [Required]
        public DateTime BillingDate { get; set; }
        public DateTime SaleDate { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public List<CreateItemDTO> Items { get; set; }

  

    }
}

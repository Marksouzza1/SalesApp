

namespace LyncasSales.Domain.DTO
{
    public class UpdateSaleDTO
    {
        public int CustomerId { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CreateItemDTO> Items { get; set; }
    }
}

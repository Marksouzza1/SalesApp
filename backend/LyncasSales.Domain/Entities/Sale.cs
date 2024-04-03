

namespace LyncasSales.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime BillingDate { get; set; } 
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItem> Items { get; set; }
        public virtual Customer Customer { get; set; }
    }
}

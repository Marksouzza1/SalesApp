using LyncasSales.Domain.Entities;

namespace LyncasSales.Domain.ViewModel
{
    public class SaleViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime SaleDate { get; set; } 
        public decimal TotalAmount { get; set; }
        public int SaleId { get; set; }
    }
}

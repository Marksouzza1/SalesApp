

namespace LyncasSales.Domain.ViewModel
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int saleId { get; set; }
    }
}

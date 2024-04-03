namespace LyncasSales.Domain.Entities
{
    public class SaleItem
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }


        public virtual Sale Sale { get; set; }
        public SaleItem() { }

        public SaleItem( string? description, decimal unitPrice, int quantity, List<SaleItem> saleItems)
        {
            Description = description;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

    }
}

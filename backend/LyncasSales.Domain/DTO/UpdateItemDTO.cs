
namespace LyncasSales.Domain.DTO
{
    internal class UpdateItemDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}

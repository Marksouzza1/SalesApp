
using LyncasSales.Domain.Entities;

namespace LyncasSales.Domain.DTO
{
    public class SaleResponseDTO
    {
        public int TotalPages { get; set; }
        public List<Sale> Sales {get; set;}
    }
}

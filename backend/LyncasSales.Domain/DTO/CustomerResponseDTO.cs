
using LyncasSales.Domain.Entities;

namespace LyncasSales.Domain.DTO
{
    public class CustomerResponseDTO
    {
        public int TotalPages { get; set; }
        public List<Customer> customers { get; set; }
    }
}

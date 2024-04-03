namespace LyncasSales.Repository.Filter
{
    public class SaleFilterParams
    {
        public int PageNumber { get; set; }
        public int PageQuantity { get; set; }
        public string SearchTerm { get; set; } = "";
    }
}

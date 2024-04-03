namespace LyncasSales.Repository.Filter
{
    public class CustomerFilterParams
    {
        public int PageNumber { get; set; }
        public int PageQuantity { get; set; } 
        public string SearchTerm { get; set; } = "";
    }
}

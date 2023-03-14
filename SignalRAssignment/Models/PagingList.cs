namespace SignalRAssignment.Models
{
    public class PagingList
    {
        public int Page { get; set; }
        public int TotalItemsInPage { get; set; }
        public string SearchValue { get; set; }
    }
}

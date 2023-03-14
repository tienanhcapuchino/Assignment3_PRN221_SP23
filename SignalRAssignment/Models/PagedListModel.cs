namespace SignalRAssignment.Models
{
    public class PagedListModel<T>
    {
        public IPagedList<T> List { get; set; }
    }
}

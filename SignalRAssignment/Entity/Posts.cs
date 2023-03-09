namespace SignalRAssignment.Entity
{
    public class Posts
    {
        public int PostID { get; set; }
        public AppUsers AppUsers { get; set; }
        public int AuthorID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PublishStatus { get; set; }
        public PostCategories PostCategories { get; set; }
        public int CategoryId { get; set; }

    }
}

namespace SignalRAssignment.Entity
{
    public class Posts
    {
        public int PostID { get; set; }
        public virtual AppUsers AppUsers { get; set; }
        public int AppUsersUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PublishStatus { get; set; }
        public virtual PostCategories PostCategories { get; set; }
        public int PostCategoriesCategoryId { get; set; }

    }
}

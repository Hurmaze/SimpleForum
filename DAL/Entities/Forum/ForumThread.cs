namespace DAL.Entities.Forum
{
    public class ForumThread : BaseEntity
    {
        public Theme? Theme { get; set; }
        public string? Title { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public string? Content { get; set; }
        public Account? Author { get; set; }
        public ICollection<Post>? ThreadPosts { get; set; }
    }
}

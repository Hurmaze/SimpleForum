namespace DAL.Entities.Forum
{
    public class Post : BaseEntity
    {
        public int AuthorId { get; set; }
        public int ThreadId { get; set; }
        public User Author { get; set; }
        public ForumThread Thread { get; set; }
        public string Content { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.UtcNow;
    }
}

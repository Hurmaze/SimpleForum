namespace DAL.Entities.Forum
{
    public class Post : BaseEntity
    {
        public User Author { get; set; }
        public ForumThread Thread { get; set; }
        public string Content { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.UtcNow;
    }
}

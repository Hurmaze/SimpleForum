namespace DAL.Entities.Forum
{
    public class Post : BaseEntity
    {
        public Account? Author { get; set; }
        public ForumThread? Thread { get; set; }
        public string? Content { get; set; }
    }
}

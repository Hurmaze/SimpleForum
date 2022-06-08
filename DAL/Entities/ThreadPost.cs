namespace DAL.Entities
{
    public class ThreadPost : BaseEntity
    {
        public Account? Author { get; set; }
        public ForumThread? Thread { get; set; }
        public string? Content { get; set; }
    }
}

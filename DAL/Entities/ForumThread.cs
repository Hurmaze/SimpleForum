using DAL.Entities.Enums;

namespace DAL.Entities
{
    public class ForumThread : BaseEntity
    {
        public Theme Theme { get; set; } = Theme.Other;
        public string? Title { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        public string? Content { get; set; }
        public Account? Author { get; set; }
        public ICollection<ThreadPost>? ThreadPosts { get; set; }
    }
}

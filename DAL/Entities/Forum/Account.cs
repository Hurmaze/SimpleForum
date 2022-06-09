using DAL.Entities.Authentication;

namespace DAL.Entities.Forum
{
    public class Account : BaseEntity
    {
        public string? Nickname { get; set; }
        public string? Email { get; set; }
        public ICollection<ForumThread>? Threads { get; set; }
        public ICollection<Post>? ThreadPosts { get; set; }
    }
}

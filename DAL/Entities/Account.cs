using DAL.Entities.Enums;

namespace DAL.Entities
{
    public class Account : BaseEntity
    {
        public string? Nickname { get; set; }
        public Role Role { get; set; } = Role.User;
        public ICollection<ForumThread>? Threads { get; set; }
        public ICollection<ThreadPost>? ThreadPosts { get; set; }
        public AccountAuth? AccountAuth { get; set; }
    }
}

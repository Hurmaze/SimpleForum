namespace DAL.Entities.Forum
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public ICollection<ForumThread> Threads { get; set; }
        public ICollection<Post> ThreadPosts { get; set; }
    }
}

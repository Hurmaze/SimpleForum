namespace DAL.Entities
{
    /// <summary>
    /// User
    /// </summary>
    public class User : BaseEntity
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationTime { get; set; } = DateTime.Now;
        public int CredentialsId { get; set; }
        public Credentials Credentials { get; set; }
        public ICollection<ForumThread> Threads { get; set; }
        public ICollection<Post> ThreadPosts { get; set; }
    }
}

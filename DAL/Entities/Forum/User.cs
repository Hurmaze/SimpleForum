namespace DAL.Entities.Forum
{
    /// <summary>
    /// User
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Nickaname
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Threads of this user
        /// </summary>
        public ICollection<ForumThread> Threads { get; set; }

        /// <summary>
        /// Posts of this user
        /// </summary>
        public ICollection<Post> ThreadPosts { get; set; }
    }
}

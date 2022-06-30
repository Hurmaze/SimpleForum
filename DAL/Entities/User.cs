namespace DAL.Entities
{
    /// <summary>
    /// User
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>
        /// The nickname.
        /// </value>
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        public Credentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets the threads.
        /// </summary>
        /// <value>
        /// The threads.
        /// </value>
        public ICollection<ForumThread> Threads { get; set; }

        /// <summary>
        /// Gets or sets the thread posts.
        /// </summary>
        /// <value>
        /// The thread posts.
        /// </value>
        public ICollection<Post> ThreadPosts { get; set; }
    }
}

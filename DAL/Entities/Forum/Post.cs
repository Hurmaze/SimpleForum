namespace DAL.Entities.Forum
{
    /// <summary>
    /// Post
    /// </summary>
    public class Post : BaseEntity
    {
        /// <summary>
        /// Id of the author
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Id of the thread
        /// </summary>
        public int ThreadId { get; set; }

        /// <summary>
        /// Author
        /// </summary>
        public User Author { get; set; }

        /// <summary>
        /// Thread
        /// </summary>
        public ForumThread Thread { get; set; }

        /// <summary>
        /// Content of the post
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// TimeCreated
        /// </summary>
        public DateTime TimeCreated { get; set; } = DateTime.UtcNow;
    }
}

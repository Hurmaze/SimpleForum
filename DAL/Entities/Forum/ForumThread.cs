namespace DAL.Entities.Forum
{
    /// <summary>
    /// ForumThread
    /// </summary>
    public class ForumThread : BaseEntity
    {
        /// <summary>
        /// Id of the theme
        /// </summary>
        public int ThemeId { get; set; }

        /// <summary>
        /// Id of the author
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Theme
        /// </summary>
        public Theme Theme { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// TimeCreated
        /// </summary>
        public DateTime TimeCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// Content of the thread
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Author
        /// </summary>
        public User Author { get; set; }

        /// <summary>
        /// Posts of this thread
        /// </summary>
        public ICollection<Post> ThreadPosts { get; set; }
    }
}

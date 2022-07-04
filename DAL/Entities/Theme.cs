namespace DAL.Entities
{
    /// <summary>
    /// Theme
    /// </summary>
    public class Theme : BaseEntity
    {
        public string ThemeName { get; set; }
        public ICollection<ForumThread> ForumThreads { get; set; }
    }
}

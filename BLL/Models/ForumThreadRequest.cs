namespace Services.Models
{
    public class ForumThreadRequest
    {
        public int? AuthorId { get; set; }
        public int? ThemeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

namespace Services.Models
{
    public class ForumThreadModel : BaseModel
    {
        public int? ThemeId { get; set; }
        public string ThemeName { get; set; }
        public string Title { get; set; }
        public DateTime TimeCreated { get; set; }
        public string Content { get; set; }
        public int? AuthorId { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorNickname { get; set; }
    }
}

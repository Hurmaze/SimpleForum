namespace BLL.Models
{
    public class ForumThreadModel : BaseModel
    {
        public string? ThemeName { get; set; }
        public string? Title { get; set; }
        public DateTime TimeCreated { get; set; }
        public string? Content { get; set; }
        public int AuthorId { get; set; }
        public ICollection<int>? ThreadPostsIds { get; set; }
    }
}

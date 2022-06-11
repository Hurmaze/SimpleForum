namespace BLL.Models
{
    public class ThemeModel : BaseModel
    {
        public string? Name { get; set; }
        public ICollection<int>? ForumThreads { get; set; }
    }
}

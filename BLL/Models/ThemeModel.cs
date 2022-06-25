namespace Services.Models
{
    public class ThemeModel : BaseModel
    {
        public string ThemeName { get; set; }
        public ICollection<int> ForumThreads { get; set; }
    }
}

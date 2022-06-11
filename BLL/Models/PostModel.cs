namespace BLL.Models
{
    public class PostModel
    {
        public int AuthorId { get; set; }
        public int ThreadId { get; set; }
        public string? Content { get; set; }
    }
}

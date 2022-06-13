namespace BLL.Models
{
    public class PostModel : BaseModel
    {
        public int AuthorId { get; set; }
        public int ThreadId { get; set; }
        public string? Content { get; set; }
    }
}

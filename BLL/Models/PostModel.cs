namespace Services.Models
{
    public class PostModel : BaseModel
    {
        public int? AuthorId { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorNickname { get; set; }
        public int ThreadId { get; set; }
        public string Content { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}

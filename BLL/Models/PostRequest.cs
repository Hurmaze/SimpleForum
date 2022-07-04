namespace Services.Models
{
    public class PostRequest
    {
        public int AuthorId { get; set; }
        public int ThreadId { get; set; }
        public string Content { get; set; }
    }
}

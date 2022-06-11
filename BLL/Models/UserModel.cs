namespace BLL.Models
{
    public class UserModel : BaseModel
    {
        public string? Nickname { get; set; }
        public string? Email { get; set; }
        public ICollection<int>? ThreadsIds { get; set; }
        public ICollection<int>? PostsIds { get; set; }
    }
}

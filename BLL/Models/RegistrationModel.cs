namespace BLL.Models
{
    public class RegistrationModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RoleName { get; set; }
        public string? Nickname { get; set; }
        public ICollection<int>? ThreadsIds { get; set; }
        public ICollection<int>? PostsIds { get; set; }
    }
}

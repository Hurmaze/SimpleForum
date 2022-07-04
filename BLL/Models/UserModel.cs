namespace Services.Models
{
    public class UserModel : BaseModel
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public int CredentialsId { get; set; }
        public string RoleName { get; set; }
        public DateTime RegistrationTime { get; set; }
        public ICollection<int> ThreadsIds { get; set; }
        public ICollection<int> PostsIds { get; set; }
    }
}

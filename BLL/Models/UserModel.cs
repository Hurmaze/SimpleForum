namespace Services.Models
{
    public class UserModel : BaseModel
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
        public int CredentialsId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}

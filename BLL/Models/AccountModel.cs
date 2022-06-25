namespace Services.Models
{
    public class AccountModel : BaseModel
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
    }
}

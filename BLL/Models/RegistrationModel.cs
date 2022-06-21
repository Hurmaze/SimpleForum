namespace BLL.Models
{
    public class RegistrationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }

        public string RoleName { get; set; }
        public string Nickname { get; set; }
    }
}

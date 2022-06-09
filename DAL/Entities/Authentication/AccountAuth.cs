using DAL.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Authentication
{
    public class AccountAuth : BaseEntity
    {
        public string? Email { get; set; }
        public Role? Role { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}

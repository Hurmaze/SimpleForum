using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class AccountAuth : BaseEntity
    {
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public Account? Account { get; set; }
        public int AccountId { get; set; }
    }
}

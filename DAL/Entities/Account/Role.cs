using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Account
{
    public class Role : BaseEntity
    {
        public string? RoleName { get; set; }
        public ICollection<Account>? Accounts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class RoleModel : BaseModel
    {
        public string RoleName { get; set; }
        public ICollection<int> CredentialsIds { get; set; }
    }
}

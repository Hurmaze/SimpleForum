using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class RoleModel : BaseModel
    {
        public string RoleName { get; set; }
        public ICollection<int> AccountAuthsIds { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    /// <summary>
    /// Role
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// Name of the role
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Accounts with this role
        /// </summary>
        public ICollection<Credentials> Accounts { get; set; }
    }

    /// <summary>
    /// Enum of basic roles
    /// </summary>
    public enum BasicRoles
    {
        /// <summary>
        /// User
        /// </summary>
        User,

        /// <summary>
        /// Admin
        /// </summary>
        Admin,

        /// <summary>
        /// Moderator
        /// </summary>
        Moderator
    }
}

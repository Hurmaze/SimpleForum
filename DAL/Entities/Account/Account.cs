using DAL.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Account
{
    /// <summary>
    /// Account entity
    /// </summary>
    public class Account : BaseEntity
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Id of the role
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Password`s hash
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Password`s salt
        /// </summary>
        public byte[] PasswordSalt { get; set; }
    }
}

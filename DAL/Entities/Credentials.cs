namespace DAL.Entities
{
    /// <summary>
    /// Account entity
    /// </summary>
    public class Credentials : BaseEntity
    {
        /// <summary>
        /// Email
        /// </summary>
        public int UserId { get; set; }

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

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }
    }
}

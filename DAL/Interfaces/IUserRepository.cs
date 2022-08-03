using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IRepository&lt;User&gt;" />
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Gets the User by email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<User> GetByEmailAsync(string email);
        /// <summary>
        /// Determines whether nickname taken or not
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <returns></returns>
        Task<bool> IsNicknameTakenAsync(string nickname);

        /// <summary>
        /// Determines whether is email exist asynchronous
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>True, if email exists and false if not.</returns>
        Task<bool> IsEmailExistAsync(string email);

        /// <summary>
        /// Gets the most active users asynchronous.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetMostActiveUsersAsync(int count);
    }
}

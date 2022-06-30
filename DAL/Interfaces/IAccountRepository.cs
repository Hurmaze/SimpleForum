using DAL.Entities;
using DAL.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface of the account repository
    /// </summary>
    /// <seealso cref="IRepository&lt;Account&gt;" />
    public interface IAccountRepository : IRepository<Credentials>
    {
        /// <summary>
        /// Determines whether is email exist asynchronous
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>True, if email exists and false if not.</returns>
        Task<bool> IsEmailExistAsync(string email);
        /// <summary>
        /// Gets the Account by email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns entity of null.</returns>
        Task<Credentials> GetByEmailAsync(string email);
    }
}

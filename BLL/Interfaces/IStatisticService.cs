using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    /// <summary>
    /// Interface for the Statistic service
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Gets the most popular threads asynchronous.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>Task&lt;IEnumerable&lt;ForumThreadModel&gt;&gt;.</returns>
        Task<IEnumerable<ForumThreadModel>> GetMostPopularThreadsAsync(int count);
        /// <summary>
        /// Gets the most active users asynchronous.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>Task&lt;IEnumerable&lt;UserModel&gt;&gt;.</returns>
        Task<IEnumerable<UserModel>> GetMostActiveUsersAsync(int count);
    }
}

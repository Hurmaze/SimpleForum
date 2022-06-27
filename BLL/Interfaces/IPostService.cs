using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    /// <summary>
    /// Interface for the Post service
    /// </summary>
    /// <seealso cref="IBaseService&lt;PostModel&gt;" />
    public interface IPostService : IBaseService<PostModel>
    {
        /// <summary>
        /// Gets the posts by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
        public Task<IEnumerable<PostModel>> GetPostsByUserIdAsync(int userId);
       
    }
}

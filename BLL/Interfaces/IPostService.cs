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
    public interface IPostService
    {
        /// <summary>
        /// Adds TModel the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;TModel&gt;</returns>
        Task<PostModel> AddAsync(PostRequest model);
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;TModel&gt;&gt;.</returns>
        Task<IEnumerable<PostModel>> GetAllAsync();

        /// <summary>
        /// Gets the TModel by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;TModel&gt;</returns>
        Task<PostModel> GetByIdAsync(int id);

        /// <summary>
        /// Updates the TModel asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task UpdateAsync(int id, PostRequest model);

        /// <summary>
        /// Deletes the TModel by identifier asynchronous.
        /// </summary>
        /// <param name="modelId">The model identifier.</param>
        /// <returns></returns>
        Task DeleteByIdAsync(int modelId);

        /// <summary>
        /// Gets the posts by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
        public Task<IEnumerable<PostModel>> GetPostsByUserIdAsync(int userId);
       
    }
}

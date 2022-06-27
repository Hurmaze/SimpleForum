using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    /// <summary>
    /// Basic interface for services
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IBaseService<TModel> where TModel : class
    {
        /// <summary>
        /// Adds TModel the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;TModel&gt;</returns>
        Task<TModel> AddAsync(TModel model);
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;TModel&gt;&gt;.</returns>
        Task<IEnumerable<TModel>> GetAllAsync();

        /// <summary>
        /// Gets the TModel by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;TModel&gt;</returns>
        Task<TModel> GetByIdAsync(int id);

        /// <summary>
        /// Updates the TModel asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task UpdateAsync(TModel model);

        /// <summary>
        /// Deletes the TModel by identifier asynchronous.
        /// </summary>
        /// <param name="modelId">The model identifier.</param>
        /// <returns></returns>
        Task DeleteByIdAsync(int modelId);
    }
}

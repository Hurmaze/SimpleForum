using DAL.Entities;

namespace DAL.Interfaces
{
    /// <summary>
    ///   <para>Basic repositroy interface</para>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>Gets all asynchronous.</summary>
        /// <returns>
        ///   IEnumerable&lt;TEntity&gt;.
        /// </returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Gets the TEntity by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Adds the TEntity asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>Deletes the by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// TEntity
        /// </returns>
        Task<TEntity> DeleteByIdAsync(int id);
    }
}

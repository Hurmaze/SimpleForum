using Services.Models;

namespace Services.Interfaces
{
    /// <summary>
    /// Interface for the ForumThread service
    /// </summary>
    /// <seealso cref="IBaseService&lt;ForumThreadModel&gt;" />
    public interface IForumThreadService
    {
        /// <summary>
        /// Adds TModel the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;TModel&gt;</returns>
        Task<ForumThreadModel> AddAsync(ForumThreadRequest model);
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;TModel&gt;&gt;.</returns>
        Task<IEnumerable<ForumThreadModel>> GetAllAsync();

        /// <summary>
        /// Gets the TModel by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;TModel&gt;</returns>
        Task<ForumThreadModel> GetByIdAsync(int id);

        /// <summary>
        /// Updates the TModel asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task UpdateAsync(int id, ForumThreadRequest model);

        /// <summary>
        /// Deletes the TModel by identifier asynchronous.
        /// </summary>
        /// <param name="modelId">The model identifier.</param>
        /// <returns></returns>
        Task DeleteByIdAsync(int modelId);

        /// <summary>
        /// Gets the thread posts asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
        Task<IEnumerable<PostModel>> GetThreadPostsAsync(int id);

        /// <summary>
        /// Gets the threads by user identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IEnumerable&lt;ForumThreadModel&gt;&gt;.</returns>
        Task<IEnumerable<ForumThreadModel>> GetThreadsByUserIdAsync(int userId);

        /// <summary>
        /// Gets all themes a synchronize.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;ThemeModel&gt;&gt;.</returns>
        Task<IEnumerable<ThemeModel>> GetAllThemesAsync();

        /// <summary>
        /// Adds the theme asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ThemeModel&gt;</returns>
        Task<ThemeModel> AddThemeAsync(ThemeModel model);

        /// <summary>
        /// Deletes the theme by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task DeleteThemeByIdAsync(int id);
    }
}

using Services.Models;

namespace Services.Interfaces
{
    /// <summary>
    /// Interface for the ForumThread service
    /// </summary>
    /// <seealso cref="IBaseService&lt;ForumThreadModel&gt;" />
    public interface IForumThreadService : IBaseService<ForumThreadModel>
    {
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

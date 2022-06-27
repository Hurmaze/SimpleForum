
namespace DAL.Interfaces
{
    /// <summary>
    /// Interface of the UnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the user repository.
        /// </summary>
        /// <value>
        /// The user repository.
        /// </value>
        public IUserRepository UserRepository { get; }
        /// <summary>
        /// Gets the account repository.
        /// </summary>
        /// <value>
        /// The account repository.
        /// </value>
        public IAccountRepository AccountRepository { get; }
        /// <summary>
        /// Gets the post repository.
        /// </summary>
        /// <value>
        /// The post repository.
        /// </value>
        public IPostRepository PostRepository { get; }
        /// <summary>
        /// Gets the forum thread repository.
        /// </summary>
        /// <value>
        /// The forum thread repository.
        /// </value>
        public IForumThreadRepository ForumThreadRepository { get; }
        /// <summary>
        /// Gets the role repository.
        /// </summary>
        /// <value>
        /// The role repository.
        /// </value>
        public IRoleRepository RoleRepository { get; }
        /// <summary>
        /// Gets the theme repository.
        /// </summary>
        /// <value>
        /// The theme repository.
        /// </value>
        public IThemeRepository ThemeRepository { get; }
        /// <summary>
        /// Saves asynchronously.
        /// </summary>
        /// <returns></returns>
        public Task SaveAsync();
    }
}
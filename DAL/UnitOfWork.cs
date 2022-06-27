using DAL.DbAccess;
using DAL.Entities.Account;
using DAL.Entities.Forum;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// The UnitOfWork
    /// </summary>
    /// <seealso cref="IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The forum database context
        /// </summary>
        private readonly ForumDbContext _forumDbContext;
        /// <summary>
        /// The account database context
        /// </summary>
        private readonly AccountDbContext _accountDbContext;
        /// <summary>
        /// The user repository
        /// </summary>
        private IUserRepository _userRepository;
        /// <summary>
        /// The account repository
        /// </summary>
        private IAccountRepository _accountRepository;
        /// <summary>
        /// The post repository
        /// </summary>
        private IPostRepository _postRepository;
        /// <summary>
        /// The forum thread repository
        /// </summary>
        private IForumThreadRepository _forumThreadRepository;
        /// <summary>
        /// The theme repository
        /// </summary>
        private IThemeRepository _themeRepository;
        /// <summary>
        /// The role repository
        /// </summary>
        private IRoleRepository _roleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="forumDbContext">The forum database context.</param>
        /// <param name="authDbContext">The authentication database context.</param>
        public UnitOfWork(ForumDbContext forumDbContext, AccountDbContext authDbContext)
        {
            _forumDbContext = forumDbContext;
            _accountDbContext = authDbContext;
            _userRepository = new UserRepository(forumDbContext);
            _accountRepository = new AccountRepository(authDbContext);
            _postRepository = new PostRepository(forumDbContext);
            _forumThreadRepository = new ForumThreadRepository(forumDbContext);
            _themeRepository = new ThemeRepository(forumDbContext);
            _roleRepository = new RoleRepository(authDbContext);
        }

        /// <summary>
        /// Gets the user repository.
        /// </summary>
        /// <value>
        /// The user repository.
        /// </value>
        public IUserRepository UserRepository { get => _userRepository; }
        /// <summary>
        /// Gets the account repository.
        /// </summary>
        /// <value>
        /// The account repository.
        /// </value>
        public IAccountRepository AccountRepository { get => _accountRepository; }
        /// <summary>
        /// Gets the post repository.
        /// </summary>
        /// <value>
        /// The post repository.
        /// </value>
        public IPostRepository PostRepository { get => _postRepository; }
        /// <summary>
        /// Gets the forum thread repository.
        /// </summary>
        /// <value>
        /// The forum thread repository.
        /// </value>
        public IForumThreadRepository ForumThreadRepository { get => _forumThreadRepository; }
        /// <summary>
        /// Gets the theme repository.
        /// </summary>
        /// <value>
        /// The theme repository.
        /// </value>
        public IThemeRepository ThemeRepository { get => _themeRepository; }
        /// <summary>
        /// Gets the role repository.
        /// </summary>
        /// <value>
        /// The role repository.
        /// </value>
        public IRoleRepository RoleRepository { get => _roleRepository; }

        /// <summary>
        /// Saves asynchronously.
        /// </summary>
        public async Task SaveAsync()
        {
            await _forumDbContext.SaveChangesAsync();
            await _accountDbContext.SaveChangesAsync();
        }
    }
}

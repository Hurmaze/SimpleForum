using DAL.DbAccess;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// User repository
    /// </summary>
    /// <seealso cref="IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The forum database context
        /// </summary>
        private readonly ForumDbContext _forumDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="forumDbContext">The forum database context.</param>
        public UserRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        /// <summary>
        /// Adds the User asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<User> AddAsync(User entity)
        {
            await _forumDbContext.Users.AddAsync(entity);

            return entity;
        }

        /// <summary>
        /// Deletes the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Task&lt;User&gt;
        /// </returns>
        public async Task<User> DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Users.FindAsync(id);

            if(entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Task&lt;IEnumerable&lt;User&gt;&gt;.
        /// </returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _forumDbContext.Users
                .Include(u => u.ThreadPosts)
                .ThenInclude(tp => tp.Thread)
                .Include(u => u.Threads)
                .Include(u => u.Credentials)
                .ThenInclude(c => c.Role)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the User by email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Task&lt;User&gt;</returns>
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _forumDbContext.Users
                .Include(u => u.ThreadPosts)
                .ThenInclude(tp => tp.Thread)
                .Include(u => u.Threads)
                .Include(u => u.Credentials)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        /// <summary>
        /// Gets the TEntity by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;User&gt;</returns>
        public async Task<User> GetByIdAsync(int id)
        {
            return await _forumDbContext.Users
                .Include(u => u.ThreadPosts)
                .ThenInclude(tp => tp.Thread)
                .Include(u => u.Threads)
                .Include(u => u.Credentials)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(u => u.Id == id); ;
        }

        public async Task<IEnumerable<User>> GetMostActiveUsersAsync(int count)
        {
            return await _forumDbContext.Users
            .OrderByDescending(x => x.ThreadPosts == null ? 0 : x.ThreadPosts.Count)
            .ThenByDescending(y => y.Threads == null ? 0 : y.Threads.Count)
            .Take(count)
            .ToListAsync();
        }

        /// <summary>
        /// Determines whether is email exist asynchronous
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// True, if email exists and false if not.
        /// </returns>
        public Task<bool> IsEmailExistAsync(string email)
        {
            return _forumDbContext.Users.AnyAsync(u => u.Email == email);
        }

        /// <summary>
        /// Determines whether nickname taken or not
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <returns>Task&lt;Bool&gt;</returns>
        public async Task<bool> IsNicknameTakenAsync(string nickname)
        {
            return await _forumDbContext.Users.AnyAsync(u =>u.Nickname == nickname);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(User entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

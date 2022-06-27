using DAL.DbAccess;
using DAL.Entities.Forum;
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
        public async Task AddAsync(User entity)
        {
            await _forumDbContext.Users.AddAsync(entity);
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
                .FirstOrDefaultAsync(u => u.Id == id); ;
        }

        /// <summary>
        /// Determines whether nickname taken or not
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <returns>Task&lt;Bool&gt;</returns>
        public async Task<bool> IsNicknameTakenAsync(string nickname)
        {
            if(nickname == null)
            {
                return false;
            }
            var nick = await _forumDbContext.Users.FirstOrDefaultAsync(u => u.Nickname == nickname);

            if (nick == null)
            {
                return false;
            }

            return true;
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

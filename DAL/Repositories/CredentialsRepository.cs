using DAL.DbAccess;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Account repository
    /// </summary>
    /// <seealso cref="ICredentialsRepository" />
    public class CredentialsRepository : ICredentialsRepository
    {
        private readonly ForumDbContext _forumDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialsRepository"/> class.
        /// </summary>
        /// <param name="forumDbContext">The authentication database context.</param>
        public CredentialsRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        /// <summary>
        /// Adds the Account asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<Credentials> AddAsync(Credentials entity)
        {
            await _forumDbContext.Credentials.AddAsync(entity);

            return entity;
        }

        /// <summary>
        /// Deletes the Account by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Task&lt;Account&gt;
        /// </returns>
        public async Task<Credentials> DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Credentials.FindAsync(id);

            if (entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Task&lt;IEnumerable&lt;Account&gt;&gt;.
        /// </returns>
        public async Task<IEnumerable<Credentials>> GetAllAsync()
        {
            return await _forumDbContext.Credentials
                .Include(c => c.Role)
                .Include(c => c.User)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the Account by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Account&gt;</returns>
        public async Task<Credentials> GetByIdAsync(int id)
        {
            return await _forumDbContext.Credentials
                .Include(c => c.Role)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Credentials> GetByUserIdAsync(int userId)
        {
            return await _forumDbContext.Credentials
                .Include(c => c.Role)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(Credentials entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

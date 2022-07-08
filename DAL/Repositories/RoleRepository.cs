using DAL.DbAccess;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Role repository
    /// </summary>
    /// <seealso cref="IRoleRepository" />
    public class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// The authentication database context
        /// </summary>
        private readonly ForumDbContext _forumDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="authDbContext">The authentication database context.</param>
        public RoleRepository(ForumDbContext authDbContext)
        {
            _forumDbContext = authDbContext;
        }

        /// <summary>
        /// Adds the Role asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<Role> AddAsync(Role entity)
        {
            await _forumDbContext.Roles.AddAsync(entity);

            return entity;
        }

        public async Task<bool> IsBasicAsync(int id)
        {
            var role = await _forumDbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);

            if(role == null)
            {
                return false;
            }

            return role.BasicRole;
        }

        /// <summary>
        /// Deletes the Role by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Role&gt;</returns>
        public async Task<Role> DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Roles.FindAsync(id);

            if (entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Role&gt;&gt;.</returns>
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _forumDbContext.Roles
                .Include(r => r.Credentials)
                .ThenInclude(c => c.User)
                .ToListAsync();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(Role entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Role&gt;</returns>
        public async Task<Role> GetByIdAsync(int id)
        {
            return await _forumDbContext.Roles
                .Include(r => r.Credentials)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}

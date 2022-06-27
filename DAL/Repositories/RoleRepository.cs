

using DAL.DbAccess;
using DAL.Entities.Account;
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
        private readonly AccountDbContext _authDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="authDbContext">The authentication database context.</param>
        public RoleRepository(AccountDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        /// <summary>
        /// Adds the Role asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task AddAsync(Role entity)
        {
            await _authDbContext.Roles.AddAsync(entity);
        }

        /// <summary>
        /// Deletes the Role by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Role&gt;</returns>
        public async Task<Role> DeleteByIdAsync(int id)
        {
            var entity = await _authDbContext.Roles.FindAsync(id);

            if (entity != null)
            {
                _authDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Role&gt;&gt;.</returns>
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _authDbContext.Roles
                .Include(r => r.Accounts)
                .ToListAsync();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(Role entity)
        {
            _authDbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Role&gt;</returns>
        public async Task<Role> GetByIdAsync(int id)
        {
            return await _authDbContext.Roles
                .Include(r => r.Accounts)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}

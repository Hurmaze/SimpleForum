using DAL.DbAccess;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Account repository
    /// </summary>
    /// <seealso cref="IAccountRepository" />
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// The authentication database context
        /// </summary>
        private readonly AccountDbContext _authDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="authDbContext">The authentication database context.</param>
        public AccountRepository(AccountDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        /// <summary>
        /// Adds the Account asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task AddAsync(Credentials entity)
        {
            await _authDbContext.Accounts.AddAsync(entity);
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
            var entity = await _authDbContext.Accounts.FindAsync(id);

            if (entity != null)
            {
                _authDbContext.Entry(entity).State = EntityState.Deleted;
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
            return await _authDbContext.Accounts
                .Include(a => a.Role)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the Account by email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// Returns entity or null.
        /// </returns>
        public async Task<Credentials> GetByEmailAsync(string email)
        {
            return await _authDbContext.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        /// <summary>
        /// Gets the Account by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Account&gt;</returns>
        public async Task<Credentials> GetByIdAsync(int id)
        {
            return await _authDbContext.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <summary>
        /// Determines whether is email exist asynchronous
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// True, if email exists and false if not.
        /// </returns>
        public async Task<bool> IsEmailExistAsync(string email)
        {
            var acc = await _authDbContext.Accounts.FirstOrDefaultAsync(x => x.Email == email);

            if (acc == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(Credentials entity)
        {
            _authDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

using DAL.DbAccess;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{

    /// <summary>
    /// Theme repository
    /// </summary>
    /// <seealso cref="IThemeRepository" />
    public class ThemeRepository : IThemeRepository
    {
        /// <summary>
        /// The forum database context
        /// </summary>
        private readonly ForumDbContext _forumDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeRepository"/> class.
        /// </summary>
        /// <param name="forumDbContext">The forum database context.</param>
        public ThemeRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        /// <summary>
        /// Adds the Theme asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<Theme> AddAsync(Theme entity)
        {
            await _forumDbContext.Themes.AddAsync(entity);

            return entity;
        }

        /// <summary>
        /// Deletes the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Theme&gt;</returns>
        public async Task<Theme> DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Themes.FindAsync(id);

            if (entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Theme&gt;&gt;.</returns>
        public async Task<IEnumerable<Theme>> GetAllAsync()
        {
            return await _forumDbContext.Themes
                .Include(t => t.ForumThreads)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Theme&gt;</returns>
        public async Task<Theme> GetByIdAsync(int id)
        {
            return await _forumDbContext.Themes
                .Include(t => t.ForumThreads)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(Theme entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

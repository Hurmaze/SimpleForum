using DAL.DbAccess;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Post repository
    /// </summary>
    /// <seealso cref="IPostRepository" />
    public class PostRepository : IPostRepository
    {
        /// <summary>
        /// The forum database context
        /// </summary>
        private readonly ForumDbContext _forumDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="PostRepository"/> class.
        /// </summary>
        /// <param name="forumDbContext">The forum database context.</param>
        public PostRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        /// <summary>
        /// Adds the Post asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<Post> AddAsync(Post entity)
        {
            await _forumDbContext.Posts.AddAsync(entity);

            return entity;
        }

        /// <summary>
        /// Deletes the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Post&gt;</returns>
        public async Task<Post> DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Posts.FindAsync(id);

            if(entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Post&gt;&gt;.</returns>
        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _forumDbContext.Posts
                .Include(p => p.Author)
                .Include(p => p.Thread)
                .ThenInclude(t => t.Author)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Post&gt;</returns>
        public async Task<Post> GetByIdAsync(int id)
        {
            return await _forumDbContext.Posts
                .Include(p => p.Author)
                .Include(p => p.Thread)
                .ThenInclude(t => t.Author)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(Post entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

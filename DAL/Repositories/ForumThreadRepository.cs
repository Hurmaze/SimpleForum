using DAL.DbAccess;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// ForumThread repository
    /// </summary>
    /// <seealso cref="IForumThreadRepository" />
    public class ForumThreadRepository : IForumThreadRepository
    {
        /// <summary>
        /// The forum database context
        /// </summary>
        private readonly ForumDbContext _forumDbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="ForumThreadRepository"/> class.
        /// </summary>
        /// <param name="forumDbContext">The forum database context.</param>
        public ForumThreadRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        /// <summary>
        /// Adds the ForumThread asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<ForumThread> AddAsync(ForumThread entity)
        {
            await _forumDbContext.Threads.AddAsync(entity);

            return entity;
        }

        /// <summary>
        /// Deletes the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ForumThread&gt;.</returns>
        public async Task<ForumThread> DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Threads.FindAsync(id);

            if (entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;ForumThread&gt;&gt;.</returns>
        public async Task<IEnumerable<ForumThread>> GetAllAsync()
        {
            return await _forumDbContext.Threads
                .Include(t => t.Author)
                .Include(t => t.Theme)
                .Include(t => t.ThreadPosts)
                .ThenInclude(p => p.Author)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ForumThread&gt;.</returns>
        public async Task<ForumThread> GetByIdAsync(int id)
        {
            return await _forumDbContext.Threads
                .Include(t => t.Author)
                .Include(t => t.Theme)
                .Include(t => t.ThreadPosts)
                .ThenInclude(p => p.Author)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Gets the most popular threads asynchronous.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>Task&lt;IEnumerable&lt;ForumThread&gt;&gt;.</returns>
        public async Task<IEnumerable<ForumThread>> GetMostPopularThreadsAsync(int count)
        {
            return await _forumDbContext.Threads
            .OrderByDescending(x => x.ThreadPosts == null ? 0 : x.ThreadPosts.Count)
            .ThenBy(y => y.TimeCreated)
            .Take(count)
            .ToListAsync();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(ForumThread entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

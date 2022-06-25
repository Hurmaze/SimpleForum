using DAL.DbAccess;
using DAL.Entities.Forum;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ForumThreadRepository : IForumThreadRepository
    {
        private readonly ForumDbContext _forumDbContext;
        public ForumThreadRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task AddAsync(ForumThread entity)
        {
            await _forumDbContext.Threads.AddAsync(entity);
        }

        public async Task<ForumThread> DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Threads.FindAsync(id);

            if (entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<ForumThread>> GetAllAsync()
        {
            return await _forumDbContext.Threads
                .Include(t => t.Author)
                .Include(t => t.Theme)
                .Include(t => t.ThreadPosts)
                .ThenInclude(p => p.Author)
                .ToListAsync();
        }

        public async Task<ForumThread> GetByIdAsync(int id)
        {
            return await _forumDbContext.Threads
                .Include(t => t.Author)
                .Include(t => t.Theme)
                .Include(t => t.ThreadPosts)
                .ThenInclude(p => p.Author)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Update(ForumThread entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

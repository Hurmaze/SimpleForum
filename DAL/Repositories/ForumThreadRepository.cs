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

        public void Delete(ForumThread entity)
        {
            if (_forumDbContext.Entry(entity).State == EntityState.Detached)
                _forumDbContext.Threads?.Attach(entity);

            _forumDbContext.Entry(entity).State = EntityState.Deleted;
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
            return await _forumDbContext.Threads.ToListAsync();
        }

        public async Task<ForumThread> GetByIdAsync(int id)
        {
            return await _forumDbContext.Threads.FindAsync(id);
        }

        public void Update(ForumThread entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

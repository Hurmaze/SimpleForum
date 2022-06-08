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
    public class PostRepository : IRepository<ThreadPost>
    {
        private readonly ForumDbContext _forumDbContext;
        public PostRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task AddAsync(ThreadPost entity)
        {
            await _forumDbContext.Posts.AddAsync(entity);
        }

        public void Delete(ThreadPost entity)
        {
            if (_forumDbContext.Entry(entity).State == EntityState.Detached)
                _forumDbContext.Posts?.Attach(entity);

            _forumDbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Posts.FindAsync(id);

            if(entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public async Task<IEnumerable<ThreadPost>> GetAllAsync()
        {
            return await _forumDbContext.Posts.ToListAsync();
        }

        public async Task<ThreadPost> GetByIdAsync(int id)
        {
            return await _forumDbContext.Posts.FindAsync(id);
        }

        public void Update(ThreadPost entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

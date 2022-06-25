﻿using DAL.DbAccess;
using DAL.Entities.Forum;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ForumDbContext _forumDbContext;
        public PostRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task AddAsync(Post entity)
        {
            await _forumDbContext.Posts.AddAsync(entity);
        }

        public async Task<Post> DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Posts.FindAsync(id);

            if(entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _forumDbContext.Posts
                .Include(p => p.Author)
                .Include(p => p.Thread)
                .ThenInclude(t => t.Author)
                .ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _forumDbContext.Posts
                .Include(p => p.Author)
                .Include(p => p.Thread)
                .ThenInclude(t => t.Author)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Post entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

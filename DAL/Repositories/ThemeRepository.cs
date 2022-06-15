using DAL.DbAccess;
using DAL.Entities.Forum;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{

    public class ThemeRepository : IThemeRepository
    {
        private readonly ForumDbContext _forumDbContext;

        public ThemeRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task AddAsync(Theme entity)
        {
            await _forumDbContext.Themes.AddAsync(entity);
        }

        public async Task<Theme> DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Themes.FindAsync(id);

            if (entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Theme>> GetAllAsync()
        {
            return await _forumDbContext.Themes
                .Include(t => t.ForumThreads)
                .ToListAsync();
        }

        public async Task<Theme> GetByIdAsync(int id)
        {
            return await _forumDbContext.Themes
                .Include(t => t.ForumThreads)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Update(Theme entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

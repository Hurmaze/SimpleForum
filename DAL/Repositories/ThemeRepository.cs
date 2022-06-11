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

        public void Delete(Theme entity)
        {
            if (_forumDbContext.Entry(entity).State == EntityState.Detached)
                _forumDbContext.Themes?.Attach(entity);

            _forumDbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Themes.FindAsync(id);

            if (entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public async Task<IEnumerable<Theme>> GetAllAsync()
        {
            return await _forumDbContext.Themes.ToListAsync();
        }

        public async Task<Theme> GetByIdAsync(int id)
        {
            return await _forumDbContext.Themes.FindAsync(id);
        }

        public void Update(Theme entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

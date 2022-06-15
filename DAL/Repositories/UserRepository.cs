using DAL.DbAccess;
using DAL.Entities.Forum;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ForumDbContext _forumDbContext;
        public UserRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task AddAsync(User entity)
        {
            await _forumDbContext.Users.AddAsync(entity);
        }

        public async Task<User> DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Users.FindAsync(id);

            if(entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _forumDbContext.Users
                .Include(u => u.ThreadPosts)
                .ThenInclude(tp => tp.Thread)
                .ToListAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _forumDbContext.Users
                .Include(u => u.ThreadPosts)
                .ThenInclude(tp => tp.Thread)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _forumDbContext.Users
                .Include(u => u.ThreadPosts)
                .ThenInclude(tp => tp.Thread)
                .FirstOrDefaultAsync(u => u.Id == id); ;
        }

        public async Task<bool> IsNicknameTakenAsync(string nickname)
        {
            return await _forumDbContext.Users.AnyAsync(u => u.Nickname == nickname);
        }

        public void Update(User entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

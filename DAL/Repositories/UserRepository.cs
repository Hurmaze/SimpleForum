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
                .Include(u => u.Threads)
                .ToListAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _forumDbContext.Users
                .Include(u => u.ThreadPosts)
                .ThenInclude(tp => tp.Thread)
                .Include(u => u.Threads)
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
            if(nickname == null)
            {
                return false;
            }
            var nick = await _forumDbContext.Users.FirstOrDefaultAsync(u => u.Nickname == nickname);

            if (nick == null)
            {
                return false;
            }

            return true;
        }

        public void Update(User entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

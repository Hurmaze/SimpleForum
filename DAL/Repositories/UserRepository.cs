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

        public void Delete(User entity)
        {
            if (_forumDbContext.Entry(entity).State == EntityState.Detached)
                _forumDbContext.Users?.Attach(entity);

            _forumDbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Users.FindAsync(id);

            if(entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _forumDbContext.Users.ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _forumDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _forumDbContext.Users.FindAsync(id);
        }

        public async Task<bool> IsNicknameTaken(string nickname)
        {
            return await _forumDbContext.Users.AnyAsync(u => u.Nickname == nickname);
        }

        public void Update(User entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

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
    public class AccountAuthenticationRepository : IRepository<AccountAuth>
    {
        private readonly ForumDbContext _forumDbContext;
        public AccountAuthenticationRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task AddAsync(AccountAuth entity)
        {
            await _forumDbContext.AccountAuths.AddAsync(entity);
        }

        public void Delete(AccountAuth entity)
        {
            if (_forumDbContext.Entry(entity).State == EntityState.Detached)
                _forumDbContext.AccountAuths?.Attach(entity);

            _forumDbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.AccountAuths.FindAsync(id);

            if (entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public async Task<IEnumerable<AccountAuth>> GetAllAsync()
        {
            return await _forumDbContext.AccountAuths.ToListAsync();
        }

        public async Task<AccountAuth> GetByIdAsync(int id)
        {
            return await _forumDbContext.AccountAuths.FindAsync(id);
        }

        public void Update(AccountAuth entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

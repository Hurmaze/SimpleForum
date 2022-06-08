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
    public class AccountRepository : IRepository<Account>
    {
        private readonly ForumDbContext _forumDbContext;
        public AccountRepository(ForumDbContext forumDbContext)
        {
            _forumDbContext = forumDbContext;
        }

        public async Task AddAsync(Account entity)
        {
            await _forumDbContext.Accounts.AddAsync(entity);
        }

        public void Delete(Account entity)
        {
            if (_forumDbContext.Entry(entity).State == EntityState.Detached)
                _forumDbContext.Accounts?.Attach(entity);

            _forumDbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _forumDbContext.Accounts.FindAsync(id);

            if(entity != null)
            {
                _forumDbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _forumDbContext.Accounts.ToListAsync();
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _forumDbContext.Accounts.FindAsync(id);
        }

        public void Update(Account entity)
        {
            _forumDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

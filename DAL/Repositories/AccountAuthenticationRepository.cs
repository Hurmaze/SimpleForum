using DAL.DbAccess;
using DAL.Entities.Authentication;
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
        private readonly AuthenticationDbContext _authDbContext;
        public AccountAuthenticationRepository(AuthenticationDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public async Task AddAsync(AccountAuth entity)
        {
            await _authDbContext.AddAsync(entity);
        }

        public void Delete(AccountAuth entity)
        {
            if (_authDbContext.Entry(entity).State == EntityState.Detached)
                _authDbContext.AccountAuths?.Attach(entity);

            _authDbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _authDbContext.AccountAuths.FindAsync(id);

            if (entity != null)
            {
                _authDbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public async Task<IEnumerable<AccountAuth>> GetAllAsync()
        {
            return await _authDbContext.AccountAuths?.ToListAsync();
        }

        public async Task<AccountAuth> GetByIdAsync(int id)
        {
            return await _authDbContext.AccountAuths.FindAsync(id);
        }

        public void Update(AccountAuth entity)
        {
            _authDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

using DAL.DbAccess;
using DAL.Entities.Account;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDbContext _authDbContext;
        public AccountRepository(AccountDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public async Task AddAsync(Account entity)
        {
            await _authDbContext.Accounts.AddAsync(entity);
        }

        public void Delete(Account entity)
        {
            if (_authDbContext.Entry(entity).State == EntityState.Detached)
                _authDbContext.Accounts?.Attach(entity);

            _authDbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<Account> DeleteByIdAsync(int id)
        {
            var entity = await _authDbContext.Accounts.FindAsync(id);

            if (entity != null)
            {
                _authDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _authDbContext.Accounts.ToListAsync();
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            return await _authDbContext.Accounts.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _authDbContext.Accounts.FindAsync(id);
        }

        public async Task<bool> IsEmailExist(string email)
        {
            var acc = await _authDbContext.Accounts.FirstOrDefaultAsync(x => x.Email == email);

            if (acc == null)
            {
                return false;
            }
            return true;
        }

        public void Update(Account entity)
        {
            _authDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

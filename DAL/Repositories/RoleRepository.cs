

using DAL.DbAccess;
using DAL.Entities.Account;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AccountDbContext _authDbContext;

        public RoleRepository(AccountDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public async Task AddAsync(Role entity)
        {
            await _authDbContext.Roles.AddAsync(entity);
        }

        public async Task<Role> DeleteByIdAsync(int id)
        {
            var entity = await _authDbContext.Roles.FindAsync(id);

            if (entity != null)
            {
                _authDbContext.Entry(entity).State = EntityState.Deleted;
            }

            return entity;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _authDbContext.Roles
                .Include(r => r.Accounts)
                .ToListAsync();
        }

        public void Update(Role entity)
        {
            _authDbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _authDbContext.Roles
                .Include(r => r.Accounts)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}

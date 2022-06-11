

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

        public void Delete(Role entity)
        {
            if (_authDbContext.Entry(entity).State == EntityState.Detached)
                _authDbContext.Roles?.Attach(entity);

            _authDbContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _authDbContext.Roles.FindAsync(id);

            if (entity != null)
            {
                _authDbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _authDbContext.Roles.ToListAsync();
        }

        public void Update(Role entity)
        {
            _authDbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _authDbContext.Roles.FindAsync(id);
        }
    }
}

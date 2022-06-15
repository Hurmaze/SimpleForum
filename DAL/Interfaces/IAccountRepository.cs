using DAL.Entities.Account;
using DAL.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<bool> IsEmailExistAsync(string email);
        Task<Account> GetByEmailAsync(string email);
    }
}

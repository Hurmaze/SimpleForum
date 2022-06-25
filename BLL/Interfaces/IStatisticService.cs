using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IStatisticService
    {
        Task<IEnumerable<ForumThreadModel>> GetMostPopularThreadsAsync(int count);
        Task<IEnumerable<UserModel>> GetMostActiveUsersAsync(int count);
    }
}

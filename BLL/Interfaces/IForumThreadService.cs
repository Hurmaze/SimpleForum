using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IForumThreadService : IBaseService<ForumThreadModel>
    {
        Task<IEnumerable<PostModel>> GetThreadPostsAsync(int id);
        Task AddNewThemeAsync(string categoryName);
    }
}

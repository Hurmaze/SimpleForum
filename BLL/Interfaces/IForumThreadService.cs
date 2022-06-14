using BLL.Models;

namespace BLL.Interfaces
{
    public interface IForumThreadService : IBaseService<ForumThreadModel>
    {
        Task<IEnumerable<PostModel>> GetThreadPostsAsync(int id);
        Task<IEnumerable<ForumThreadModel>> GetThreadsByUserIdAsync(int userId);
        Task AddNewThemeAsync(string themeName);
        Task DeleteThemeByIdAsync(int id);
    }
}

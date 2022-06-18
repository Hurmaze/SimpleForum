using BLL.Models;

namespace BLL.Interfaces
{
    public interface IForumThreadService : IBaseService<ForumThreadModel>
    {
        Task<IEnumerable<PostModel>> GetThreadPostsAsync(int id);
        Task<IEnumerable<ForumThreadModel>> GetThreadsByUserIdAsync(int userId);
        Task<IEnumerable<ForumThreadModel>> GetMostPopularAsync(int count);
        Task<ThemeModel> AddThemeAsync(ThemeModel model);
        Task DeleteThemeByIdAsync(int id);
    }
}

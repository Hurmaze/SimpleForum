using Services.Models;

namespace Services.Interfaces
{
    public interface IForumThreadService : IBaseService<ForumThreadModel>
    {
        Task<IEnumerable<PostModel>> GetThreadPostsAsync(int id);
        Task<IEnumerable<ForumThreadModel>> GetThreadsByUserIdAsync(int userId);
        Task<ThemeModel> AddThemeAsync(ThemeModel model);
        Task DeleteThemeByIdAsync(int id);
    }
}

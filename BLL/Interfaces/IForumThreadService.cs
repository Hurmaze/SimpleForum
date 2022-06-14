﻿using BLL.Models;

namespace BLL.Interfaces
{
    public interface IForumThreadService : IBaseService<ForumThreadModel>
    {
        Task<IEnumerable<PostModel>> GetThreadPostsAsync(int id);
        Task<IEnumerable<ForumThreadModel>> GetThreadsByUserIdAsync(int userId);
        Task<ThemeModel> AddNewThemeAsync(ThemeModel model);
        Task DeleteThemeByIdAsync(int id);
    }
}

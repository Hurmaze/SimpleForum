﻿using DAL.Entities;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface of the theme repository.
    /// </summary>
    /// <seealso cref="IRepository&lt;Theme&gt;" />
    public interface IThemeRepository : IRepository<Theme>
    {
        Task<bool> IsExistAsync(string themeName);
    }
}

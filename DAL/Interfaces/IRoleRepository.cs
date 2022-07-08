using DAL.Entities;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface of the role repository
    /// </summary>
    /// <seealso cref="IRepository&lt;Role&gt;" />
    public interface IRoleRepository : IRepository<Role>
    {
        Task<bool> IsBasicAsync(int id);
    }
}

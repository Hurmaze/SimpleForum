using DAL.Entities;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface of the account repository
    /// </summary>
    /// <seealso cref="IRepository&lt;Account&gt;" />
    public interface ICredentialsRepository : IRepository<Credentials>
    {
        Task<Credentials> GetByUserIdAsync(int userId);
    }
}

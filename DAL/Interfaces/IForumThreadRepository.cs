using DAL.Entities;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface of the ForumThread repository
    /// </summary>
    /// <seealso cref="IRepository&lt;ForumThread&gt;" />
    public interface IForumThreadRepository : IRepository<ForumThread>
    {
        Task<IEnumerable<ForumThread>> GetMostPopularThreadsAsync(int count);
    }
}

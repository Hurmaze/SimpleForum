
namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IAccountRepository AccountRepository { get; }
        public IPostRepository PostRepository { get; }
        public IForumThreadRepository ForumThreadRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IThemeRepository ThemeRepository { get; }
        public Task SaveAsync();
    }
}
using DAL.DbAccess;
using DAL.Entities.Authentication;
using DAL.Entities.Forum;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForumDbContext _forumDbContext;
        private IRepository<Account> _accountRepository;
        private IRepository<AccountAuth> _accountAuthRepository;
        private IRepository<Post> _postRepository;
        private IRepository<ForumThread> _forumThreadRepository;

        public UnitOfWork(ForumDbContext forumDbContext, AuthenticationDbContext authDbContext)
        {
            _forumDbContext = forumDbContext;
            _accountRepository = new AccountRepository(forumDbContext);
            _accountAuthRepository = new AccountAuthenticationRepository(authDbContext);
            _postRepository = new PostRepository(forumDbContext);
            _forumThreadRepository = new ForumThreadRepository(forumDbContext);
        }

        public IRepository<Account> AccountRepository { get => _accountRepository; }
        public IRepository<AccountAuth> AccountAuthRepository { get => _accountAuthRepository; }
        public IRepository<Post> PostRepository { get => _postRepository; }
        public IRepository<ForumThread> ForumThreadRepository { get => _forumThreadRepository; }

        public async Task SaveAsync()
        {
            await _forumDbContext.SaveChangesAsync();
        }
    }
}

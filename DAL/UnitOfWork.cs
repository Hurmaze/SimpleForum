using DAL.DbAccess;
using DAL.Entities.Account;
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
        private readonly AccountDbContext _accountDbContext;
        private IUserRepository _userRepository;
        private IAccountRepository _accountRepository;
        private IPostRepository _postRepository;
        private IForumThreadRepository _forumThreadRepository;

        public UnitOfWork(ForumDbContext forumDbContext, AccountDbContext authDbContext)
        {
            _forumDbContext = forumDbContext;
            _accountDbContext = authDbContext;
            _userRepository = new UserRepository(forumDbContext);
            _accountRepository = new AccountRepository(authDbContext);
            _postRepository = new PostRepository(forumDbContext);
            _forumThreadRepository = new ForumThreadRepository(forumDbContext);
        }

        public IUserRepository UserRepository { get => _userRepository; }
        public IAccountRepository AccountRepository { get => _accountRepository; }
        public IPostRepository PostRepository { get => _postRepository; }
        public IForumThreadRepository ForumThreadRepository { get => _forumThreadRepository; }

        public async Task SaveAsync()
        {
            await _forumDbContext.SaveChangesAsync();
        }
    }
}

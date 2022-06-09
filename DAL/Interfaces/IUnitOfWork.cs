using DAL.Entities.Authentication;
using DAL.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Account> AccountRepository { get; }
        public IRepository<AccountAuth> AccountAuthRepository { get; }
        public IRepository<Post> PostRepository { get; }
        public IRepository<ForumThread> ForumThreadRepository { get; }
        public Task SaveAsync();
    }
}
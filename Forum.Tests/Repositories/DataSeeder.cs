using DAL.DbAccess;
using DAL.Entities.Account;
using DAL.Entities.Forum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Tests.Repositories
{
    internal class DataSeeder
    {
        public static DbContextOptions<ForumDbContext> GetForumDbOptions()
        {
            var options = new DbContextOptionsBuilder<ForumDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ForumDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        public static DbContextOptions<AccountDbContext> GetAccountDbOptions()
        {
            var options = new DbContextOptionsBuilder<AccountDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new AccountDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        private static void SeedData(ForumDbContext context)
        {
            var users = new List<User>
            {
                    new User { Email = "email1@gmail.com", Nickname = "nickname1" },
                    new User { Email = "email2@gmail.com", Nickname = "nickname2" },
                    new User { Email = "email3@gmail.com", Nickname = "nickname3" },
                    new User { Email = "email4@gmail.com", Nickname = "nickname4" },
                    new User { Email = "email5@gmail.com", Nickname = "nickname5" }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            var themes = new List<Theme>
            {
                    new Theme { ThemeName = "Books" },
                    new Theme { ThemeName = "Elephants" }
            };
            context.Themes.AddRange(themes);
            context.SaveChanges();

            var threads = new List<ForumThread>
            {
                new ForumThread { Author = users[0], Content = "Some text", Title = "Super elephants", Theme = themes[1], TimeCreated = DateTime.Now },
                new ForumThread { Author = users[3], Content = "My first book was...", Title = "Man I love books", Theme = themes[0], TimeCreated = DateTime.Now }
            };
            context.Threads.AddRange(threads);
            context.SaveChanges();

            var posts = new List<Post>
            {
                new Post { Thread = threads[0], Content = "Man i love elephants!", Author = users[1], TimeCreated = DateTime.Now },
                new Post { Thread = threads[0], Content = "My favourite elephant is...", Author = users[2], TimeCreated = DateTime.Now},
                new Post { Thread = threads[1], Content = "Books are great you know.", Author = users[4], TimeCreated = DateTime.Now},
                new Post { Thread = threads[1], Content = "Read recently about Segriy Zhadan... He is cool.", Author = users[0], TimeCreated = DateTime.Now}
            };
            context.Posts.AddRange(posts);
            context.SaveChanges();
        }

        public static void SeedData(AccountDbContext context)
        {
            var roles = new List<Role>
            {
                new Role { RoleName = "user"},
                new Role { RoleName = "admin"}
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();

            var accounts = new List<Account>
            {
                new Account { Role = roles[0], Email = "email1@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Account { Role = roles[0], Email = "email2@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Account { Role = roles[0], Email = "email3@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Account { Role = roles[1], Email = "email4@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Account { Role = roles[1], Email = "email5@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }}
            };
            context.Accounts.AddRange(accounts);
            context.SaveChanges();
        }
    }
}

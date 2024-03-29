﻿using DAL.DbAccess;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Forum.Tests.Helpers
{
    internal class RepositoryHelper
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

        private static void SeedData(ForumDbContext context)
        {
            var roles = new List<Role>
            {
                new Role { RoleName = "user"},
                new Role { RoleName = "admin"}
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();

            var users = new List<User>
            {
                    new User { Email = "email1@gmail.com", Nickname = "nickname1" },
                    new User { Email = "email2@gmail.com", Nickname = "nickname2" },
                    new User { Email = "email3@gmail.com", Nickname = "nickname3" },
                    new User { Email = "email4@gmail.com", Nickname = "nickname4" },
                    new User { Email = "email5@gmail.com", Nickname = "nickname5" }
            };
            context.Users.AddRange(users);

            var accounts = new List<Credentials>
            {
                new Credentials { Role = roles[0], UserId=1, PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Role = roles[0], UserId=2, PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Role = roles[0], UserId=3, PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Role = roles[1], UserId=4, PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Role = roles[1], UserId=5, PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }}
            };
            context.Credentials.AddRange(accounts);
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
    }
}

using DAL.DbAccess;
using DAL.Entities;
using DAL.Repositories;
using Forum.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Tests.Repositories
{
    internal class ForumThreadRepositoryTests
    {
        static List<Theme> themes = new List<Theme>
        {
            new Theme { ThemeName = "Books" },
            new Theme { ThemeName = "Elephants" }
        };

        static List<User> users = new List<User>
        {
            new User { Email = "email1@gmail.com", Nickname = "nickname1" },
            new User { Email = "email2@gmail.com", Nickname = "nickname2" },
            new User { Email = "email3@gmail.com", Nickname = "nickname3" },
            new User { Email = "email4@gmail.com", Nickname = "nickname4" },
            new User { Email = "email5@gmail.com", Nickname = "nickname5" }
        };

        static List<ForumThread> expectedForumThreads = new List<ForumThread>
        {
            new ForumThread { Id = 1, Author = users[0], Content = "Some text", Title = "Super elephants", Theme = themes[1], TimeCreated = DateTime.Now },
            new ForumThread { Id = 2 ,Author = users[3], Content = "My first book was...", Title = "Man I love books", Theme = themes[0], TimeCreated = DateTime.Now }
        };


        [TestCase(1)]
        [TestCase(2)]
        public async Task ForumThreadRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new ForumDbContext(RepositoryHelper.GetForumDbOptions());

            var threadRepository = new ForumThreadRepository(context);

            var thread = await threadRepository.GetByIdAsync(id);

            var expected = expectedForumThreads.FirstOrDefault(x => x.Id == id);

            Assert.That(thread, Is.EqualTo(expected).Using(new ForumThreadEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task ForumThreadRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new ForumDbContext(RepositoryHelper.GetForumDbOptions());

            var threadRepository = new ForumThreadRepository(context);
            var threads = await threadRepository.GetAllAsync();
            threads = threads.OrderBy(x => x.Id);

            Assert.That(threads, Is.EqualTo(expectedForumThreads).Using(new ForumThreadEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task ForumThreadRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new ForumDbContext(RepositoryHelper.GetForumDbOptions());

            var threadRepository = new ForumThreadRepository(context);
            var thread = new ForumThread {Content = "Content", Title = "Title"};

            await threadRepository.AddAsync(thread);
            await context.SaveChangesAsync();

            Assert.That(context.Threads.Count(), Is.EqualTo(expectedForumThreads.Count + 1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task ForumThreadRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(RepositoryHelper.GetForumDbOptions());

            var threadRepository = new ForumThreadRepository(context);

            await threadRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Threads.Count(), Is.EqualTo(expectedForumThreads.Count - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public void ForumThreadRepository_Update_TimesOnce()
        {
            using var context = new ForumDbContext(RepositoryHelper.GetForumDbOptions());

            var threadRepository = new ForumThreadRepository(context);
            var thread = new ForumThread { Id = 1, Content = "NewContent" };

            Assert.DoesNotThrow(() => threadRepository.Update(thread));

        }
    }
}

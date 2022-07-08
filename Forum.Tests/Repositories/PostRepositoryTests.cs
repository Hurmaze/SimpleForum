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
    internal class PostRepositoryTests
    {
        #region data
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

        static List<ForumThread> threads = new List<ForumThread>
        {
            new ForumThread { Author = users[0], Content = "Some text", Title = "Super elephants", Theme = themes[1], TimeCreated = DateTime.Now },
            new ForumThread { Author = users[3], Content = "My first book was...", Title = "Man I love books", Theme = themes[0], TimeCreated = DateTime.Now }
        };

        static List<Post> expectedPosts = new List<Post>
        {
            new Post { Id = 1, Thread = threads[0], Content = "Man i love elephants!", Author = users[1], TimeCreated = DateTime.Now },
            new Post { Id = 2, Thread = threads[0], Content = "My favourite elephant is...", Author = users[2], TimeCreated = DateTime.Now},
            new Post { Id = 3, Thread = threads[1], Content = "Books are great you know.", Author = users[4], TimeCreated = DateTime.Now},
            new Post { Id = 4, Thread = threads[1], Content = "Read recently about Segriy Zhadan... He is cool.", Author = users[0], TimeCreated = DateTime.Now}
        };

        #endregion

        [TestCase(1)]
        [TestCase(2)]
        public async Task PostRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new ForumDbContext(RepositoryHelper.GetForumDbOptions());

            var postRepository = new PostRepository(context);

            var post = await postRepository.GetByIdAsync(id);

            var expected = expectedPosts.FirstOrDefault(x => x.Id == id);

            Assert.That(post, Is.EqualTo(expected).Using(new PostEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task PostRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new ForumDbContext(RepositoryHelper.GetForumDbOptions());

            var postRepository = new PostRepository(context);
            var posts = await postRepository.GetAllAsync();
            posts = posts.OrderBy(x => x.Id);

            Assert.That(posts, Is.EqualTo(expectedPosts).Using(new PostEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task PostRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new ForumDbContext(RepositoryHelper.GetForumDbOptions());

            var postRepository = new PostRepository(context);
            var post = new Post { Content = "Bananas" };

            await postRepository.AddAsync(post);
            await context.SaveChangesAsync();

            Assert.That(context.Posts.Count(), Is.EqualTo(expectedPosts.Count + 1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task PostRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(RepositoryHelper.GetForumDbOptions());

            var postRepository = new PostRepository(context);

            await postRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Posts.Count(), Is.EqualTo(expectedPosts.Count - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public void PostRepository_Update_TimesOnce()
        {
            using var context = new ForumDbContext(RepositoryHelper.GetForumDbOptions());

            var postRepository = new PostRepository(context);
            var post = new Post { Id = 1, Content = "NewName" };

            Assert.DoesNotThrow(() => postRepository.Update(post));

        }
    }
}

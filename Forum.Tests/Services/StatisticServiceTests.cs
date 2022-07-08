using Services.Models;
using Services.Services;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using DAL.Entities;
using Forum.Tests.Helpers;

namespace Forum.Tests.Services
{
    internal class StatisticServiceTests
    {
        ServiceHelper data;

        [Test]
        public async Task StatisticService_GetMostPopularAsync_ReturnsCorrectValues()
        {
            data = new ServiceHelper();
            var returnData = data.GetForumThreadEntities;
            returnData[0].ThreadPosts = new List<Post> { data.GetPostEntities[0], data.GetPostEntities[1] };
            returnData[1].ThreadPosts = new List<Post> { data.GetPostEntities[2], data.GetPostEntities[3] };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ForumThreadRepository.GetAllAsync())
                .ReturnsAsync(returnData);
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new StatisticService(mockUnitOfWork.Object, data.CreateMapperProfile());

            var expected = new List<ForumThreadModel> { data.GetForumThreadModels[0] };
            

            var threads = await forumThreadService.GetMostPopularThreadsAsync(1);

            mockUnitOfWork.Verify(x => x.ForumThreadRepository.GetAllAsync(), Times.Once());
            Assert.NotNull(threads);
            Assert.That(threads, Is.EqualTo(expected).Using(new ForumThreadModelEqualityComparer()), message: "GetMostPopularAsync method works incorrect");
        }

        [Test]
        public async Task UserService_GetMostPopularAsync_ReturnsCorrectValues()
        {
            data = new ServiceHelper();
            List<User> returnData = new List<User>
            {
                new User { Id = 1, Email = "email1@gmail.com", Nickname = "nickname1", Threads = new List<ForumThread> { new ForumThread() { Id = 1} }, ThreadPosts = new List<Post> { new Post() { Id = 1}, new Post() { Id = 2} } },
                new User { Id = 2, Email = "email2@gmail.com", Nickname = "nickname2", Threads = new List<ForumThread>(), ThreadPosts = new List<Post> { new Post(), new Post() } },
                new User { Id = 3, Email = "email3@gmail.com", Nickname = "nickname3", Threads = new List<ForumThread>(), ThreadPosts = new List<Post> { new Post() } },
                new User { Id = 4, Email = "email4@gmail.com", Nickname = "nickname4", Threads = new List<ForumThread> {new ForumThread() }, ThreadPosts = new List<Post> { new Post() } },
                new User { Id = 5, Email = "email5@gmail.com", Nickname = "nickname5",Threads = new List<ForumThread>(), ThreadPosts = new List<Post>() }
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.GetAllAsync())
                .ReturnsAsync(returnData);

            var userAccountService = new StatisticService(mockUnitOfWork.Object, data.CreateMapperProfile());

            var expected = new List<UserModel> { new UserModel { Id = 1, Email = "email1@gmail.com", Nickname = "nickname1" } };

            var threads = await userAccountService.GetMostActiveUsersAsync(1);

            mockUnitOfWork.Verify(x => x.UserRepository.GetAllAsync(), Times.Once());
            Assert.NotNull(threads);
            Assert.That(threads, Is.EqualTo(expected).Using(new UserModelEqualityComparer()), message: "GetMostPopularAsync method works incorrect");
        }
    }
}

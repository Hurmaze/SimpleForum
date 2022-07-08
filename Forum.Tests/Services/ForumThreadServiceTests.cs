using Services.Models;
using Services.Services;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Services.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Forum.Tests.Helpers;

namespace Forum.Tests.Services
{
    internal class ForumThreadServiceTests
    {
        ServiceHelper data;

        [Test]
        public async Task ForumThreadService_AddAsync_PostAdded()
        {
            data = new ServiceHelper();

            var forumThreadRequest = new ForumThreadRequest { AuthorId = 1, Content = "kekd", ThemeId = 1, Title = "kekd"};
            var forumThreadEntity = new ForumThread { Id = 6, AuthorId = 1, Content = "kekd", ThemeId = 1, Title = "kekd" };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ForumThreadRepository.AddAsync(It.IsAny<ForumThread>()))
                .ReturnsAsync(forumThreadEntity);
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await forumThreadService.AddAsync(forumThreadRequest);

            mockUnitOfWork.Verify(x => x.ForumThreadRepository.AddAsync(It.IsAny<ForumThread>()), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task ForumThreadService_DeleteByIdAsync_PostDeleted(int id)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ForumThreadRepository.DeleteByIdAsync(id))
                .Returns(Task.FromResult(data.GetForumThreadEntities[id - 1]));
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await forumThreadService.DeleteByIdAsync(id);

            mockUnitOfWork.Verify(x => x.ForumThreadRepository.DeleteByIdAsync(id), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task ForumThreadService_DeleteByIdAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ForumThreadRepository.DeleteByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((ForumThread)null);
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => forumThreadService.DeleteByIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task ForumThreadService_GetAllAsync_ReturnAllPosts()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.ForumThreadRepository.GetAllAsync()).ReturnsAsync(data.GetForumThreadEntities);
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var posts = await forumThreadService.GetAllAsync();

            mockUnitOfWork.Verify(x => x.ForumThreadRepository.GetAllAsync(), Times.Once());
            Assert.NotNull(posts);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task ForumThreadService_GetByIdAsync_ReturnPost(int id)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.ForumThreadRepository.GetByIdAsync(id))
                .ReturnsAsync(data.GetForumThreadEntities[id - 1]);
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var post = await forumThreadService.GetByIdAsync(id);

            mockUnitOfWork.Verify(x => x.ForumThreadRepository.GetByIdAsync(id), Times.Once());
            Assert.NotNull(post);
        }

        [Test]
        public async Task ForumThreadService_GetByIdAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ForumThreadRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((ForumThread)null);
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => forumThreadService.GetByIdAsync(It.IsAny<int>()));
        }

        [TestCase(2)]
        public async Task Postservice_GetPostsByUserIdAsync_ReturnsPosts(int userId)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(userId))
                .ReturnsAsync(data.GetUserEntities[userId - 1]);
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var forumThreads = await forumThreadService.GetThreadsByUserIdAsync(userId);

            mockUnitOfWork.Verify(x => x.UserRepository.GetByIdAsync(userId), Times.Once());
            Assert.NotNull(forumThreads);
        }

        [Test]
        public async Task Postservice_GetPostsByUserIdAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => forumThreadService.GetThreadsByUserIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task ForumThreadService_UpdateAsync_Updates()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.ForumThreadRepository.Update(It.IsAny<ForumThread>()));
            mockUnitOfWork.Setup(m => m.ForumThreadRepository.GetByIdAsync(1))
                .ReturnsAsync(data.GetForumThreadEntities[0]);

            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await forumThreadService.UpdateAsync(data.GetForumThreadModels[0].Id, data.GetForumThreadRequests[0]);

            mockUnitOfWork.Verify(x => x.ForumThreadRepository.Update(It.IsAny<ForumThread>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task ForumThreadService_UpdateAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.ForumThreadRepository.Update(It.IsAny<ForumThread>()));
            mockUnitOfWork.Setup(m => m.ForumThreadRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((ForumThread)null);

            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => forumThreadService.UpdateAsync(data.GetForumThreadModels[0].Id, data.GetForumThreadRequests[0]));
        }

        [Test]
        public async Task ForumThreadService_AddThemeAsync_AddsTheme()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.ThemeRepository.GetAllAsync())
                .ReturnsAsync(data.GetThemeEntities);

            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var theme = new ThemeModel { ThemeName = "new theme" };

            await forumThreadService.AddThemeAsync(theme);

            mockUnitOfWork.Verify(x => x.ThemeRepository.AddAsync(It.IsAny<Theme>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task ForumThreadService_AddThemeAsync_AddsFirstTheme()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.ThemeRepository.GetAllAsync())
                .ReturnsAsync((IEnumerable<Theme>)null);

            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var theme = new ThemeModel { ThemeName = "new theme" };

            await forumThreadService.AddThemeAsync(theme);

            mockUnitOfWork.Verify(x => x.ThemeRepository.AddAsync(It.IsAny<Theme>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task ForumThreadService_AddThemeAsync_AlreadyExistException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.ThemeRepository.GetAllAsync())
                .ReturnsAsync(data.GetThemeEntities);

            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<AlreadyExistException>(() => forumThreadService.AddThemeAsync(data.GetThemeModels[0]));
        }

        [TestCase(1)]
        public async Task ForumThreadService_DeleteThemeByIdAsync_ThemeDeleted(int id)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ThemeRepository.DeleteByIdAsync(id))
                .ReturnsAsync(data.GetThemeEntities[id - 1]);

            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await forumThreadService.DeleteThemeByIdAsync(id);

            mockUnitOfWork.Verify(x => x.ThemeRepository.DeleteByIdAsync(id), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task ForumThreadService_DeleteThemeByIdAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ThemeRepository.DeleteByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Theme)null);
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => forumThreadService.DeleteThemeByIdAsync(It.IsAny<int>()));
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task ForumThreadService_GetThreadPostsAsync_ReturnPost(int id)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.ForumThreadRepository.GetByIdAsync(id))
                .ReturnsAsync(data.GetForumThreadEntities[id - 1]);
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var post = await forumThreadService.GetThreadPostsAsync(id);

            mockUnitOfWork.Verify(x => x.ForumThreadRepository.GetByIdAsync(id), Times.Once());
            Assert.NotNull(post);
        }

        [Test]
        public async Task ForumThreadService_GetThreadPostsAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.ForumThreadRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((ForumThread)null);
            var mockLogger = new Mock<ILogger<ForumThreadService>>();
            var forumThreadService = new ForumThreadService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => forumThreadService.GetThreadPostsAsync(It.IsAny<int>()));
        }
    }
}

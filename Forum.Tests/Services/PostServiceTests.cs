using Services.Models;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Services.Services;
using Microsoft.Extensions.Logging;
using Services.Validation.Exceptions;
using DAL.Entities;

namespace Forum.Tests.Services
{
    public class PostServiceTests
    {
        Data data;

        [Test]
        public async Task PostService_AddAsync_PostAdded()
        {
            data = new Data();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PostRepository.AddAsync(It.IsAny<Post>()));
            var mockLogger = new Mock<ILogger<PostService>>();
            var postService = new PostService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var post = new PostModel { AuthorId = 1, Content = "content", ThreadId = 1, TimeCreated = DateTime.Now };

            await postService.AddAsync(post);

            mockUnitOfWork.Verify(x => x.PostRepository.AddAsync(It.IsAny<Post>()), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task PostService_DeleteByIdAsync_PostDeleted(int id)
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PostRepository.DeleteByIdAsync(id))
                .ReturnsAsync(data.GetPostEntities[id - 1]);
            var mockLogger = new Mock<ILogger<PostService>>();
            var postService = new PostService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await postService.DeleteByIdAsync(id);

            mockUnitOfWork.Verify(x => x.PostRepository.DeleteByIdAsync(id), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task PostService_DeleteByIdAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PostRepository.DeleteByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Post)null);
            var mockLogger = new Mock<ILogger<PostService>>();
            var postService = new PostService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => postService.DeleteByIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task PostService_GetAllAsync_ReturnAllPosts()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.PostRepository.GetAllAsync())
                .ReturnsAsync(data.GetPostEntities);
            var mockLogger = new Mock<ILogger<PostService>>();
            var postService = new PostService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var posts = await postService.GetAllAsync();

            mockUnitOfWork.Verify(x => x.PostRepository.GetAllAsync(), Times.Once());
            Assert.NotNull(posts);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task PostService_GetByIdAsync_ReturnPost(int id)
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.PostRepository.GetByIdAsync(id))
                .ReturnsAsync(data.GetPostEntities[id-1]);
            var mockLogger = new Mock<ILogger<PostService>>();
            var postService = new PostService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var post = await postService.GetByIdAsync(id);

            mockUnitOfWork.Verify(x => x.PostRepository.GetByIdAsync(id), Times.Once());
            Assert.NotNull(post);
        }

        [Test]
        public async Task PostService_GetByIdAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PostRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Post)null);
            var mockLogger = new Mock<ILogger<PostService>>();
            var postService = new PostService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => postService.GetByIdAsync(It.IsAny<int>()));
        }

        [TestCase(2)]
        public async Task Postservice_GetPostsByUserIdAsync_ReturnsPosts(int userId)
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(userId))
                .ReturnsAsync(data.GetUserEntities[userId - 1]);
            var mockLogger = new Mock<ILogger<PostService>>();
            var postService = new PostService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var post = await postService.GetPostsByUserIdAsync(userId);

            mockUnitOfWork.Verify(x => x.UserRepository.GetByIdAsync(userId), Times.Once());
            Assert.NotNull(post);
        }

        [Test]
        public async Task Postservice_GetPostsByUserIdAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);
            var mockLogger = new Mock<ILogger<PostService>>();
            var postService = new PostService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => postService.GetPostsByUserIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task PostService_UpdateAsync_Updates()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PostRepository.Update(It.IsAny<Post>()));
            mockUnitOfWork.Setup(m => m.PostRepository.GetByIdAsync(1))
                .ReturnsAsync(data.GetPostEntities[0]);
            var mockLogger = new Mock<ILogger<PostService>>();
            var postService = new PostService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await postService.UpdateAsync(data.GetPostModels[0]);

            mockUnitOfWork.Verify(x => x.PostRepository.Update(It.IsAny<Post>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task PostService_UpdateAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.PostRepository.Update(It.IsAny<Post>()));
            mockUnitOfWork.Setup(m => m.PostRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Post)null);
            var mockLogger = new Mock<ILogger<PostService>>();
            var postService = new PostService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => postService.UpdateAsync(data.GetPostModels[0]));
        }
    }
}


﻿using Services;
using Services.Models;
using Services.Services;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Services.Validation.Exceptions;
using DAL.Entities;
using Forum.Tests.Helpers;

namespace Forum.Tests.Services
{
    internal class UserServiceTests
    {
        ServiceHelper data;

        [Test]
        public async Task UserService_RegisterAsync_Registered()
        {
            data = new ServiceHelper();

            var registerModel = new RegistrationModel { Email = "valid@email.com", Nickname = "SuperNIckname", Password = "Passw0rd", PasswordRepeat = "Passw0rd"};
            
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.AddAsync(It.IsAny<User>()));
            mockUnitOfWork.Setup(m => m.RoleRepository.GetAllAsync())
                .ReturnsAsync(data.GetRoleEntities);
            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await userAccountService.RegisterAsync(registerModel);

            mockUnitOfWork.Verify(x => x.UserRepository.AddAsync(It.IsAny<User>()), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UserService_RegisterAsync_ThrowsInvalidRegistrationException()
        {
            data = new ServiceHelper();

            var registerModel = new RegistrationModel { Email = "valid@email.com", Nickname = "SuperNIckname", Password = "Passw0rd", PasswordRepeat = "Passw0rd"};

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.IsEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<InvalidRegistrationException>(() => userAccountService.RegisterAsync(registerModel));
        }

        [Test]
        public async Task UserService_RegisterAsync_ThrowsNicknameException()
        {
            data = new ServiceHelper();

            var registerModel = new RegistrationModel { Email = "valid@email.com", Nickname = "SuperNIckname", Password = "Passw0rd", PasswordRepeat = "Passw0rd" };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.IsEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            mockUnitOfWork.Setup(m => m.UserRepository.IsNicknameTakenAsync(It.IsAny<string>()))
               .ReturnsAsync(true);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NicknameException>(() => userAccountService.RegisterAsync(registerModel));
        }

        [TestCase(2)]
        public async Task UserService_DeleteByIdAsync_UserAccountDeleted(int id)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(id))
                .ReturnsAsync(data.GetUserEntities[id - 1]);

            mockUnitOfWork.Setup(m => m.UserRepository.DeleteByIdAsync(id));
                
            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetUserEntities[id-1]);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await userAccountService.DeleteByIdAsync(id);

            mockUnitOfWork.Verify(x => x.UserRepository.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserService_DeleteByIdAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.DeleteByIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task UserService_GetAllAsync_ReturnAllUsers()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetAllAsync())
                .ReturnsAsync(data.GetUserEntities);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var posts = await userAccountService.GetAllAsync();

            mockUnitOfWork.Verify(x => x.UserRepository.GetAllAsync(), Times.Once());
            Assert.NotNull(posts);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task UserService_GetByIdAsync_ReturnUserModel(int id)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(id))
                .ReturnsAsync(data.GetUserEntities[id - 1]);
            var email = data.GetUserEntities[id - 1].Email;

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var post = await userAccountService.GetByIdAsync(id);

            mockUnitOfWork.Verify(x => x.UserRepository.GetByIdAsync(id), Times.Once());
            Assert.NotNull(post);
        }

        [Test]
        public async Task UserService_GetByIdAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.GetByIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task UserService_GetAllRolesAsync_ReturnAllRoles()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.RoleRepository.GetAllAsync())
                .ReturnsAsync(data.GetRoleEntities);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            var posts = await userAccountService.GetAllRolesAsync();

            mockUnitOfWork.Verify(x => x.RoleRepository.GetAllAsync(), Times.Once());
            Assert.NotNull(posts);
        }

        [TestCase(2)]
        public async Task UserService_DeleteRoleAsync_RoleDeleted(int id)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.RoleRepository.DeleteByIdAsync(id))
                .ReturnsAsync(data.GetRoleEntities[id -1]);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await userAccountService.DeleteRoleAsync(id);

            mockUnitOfWork.Verify(x => x.RoleRepository.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserService_DeleteRoleAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.RoleRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Role)null);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.DeleteRoleAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task UserService_DeleteRoleAsync_ThrowsProhibitedOperationException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.RoleRepository.IsBasicAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<ProhibitedOperationException>(() => userAccountService.DeleteRoleAsync(It.IsAny<int>()));
        }

        [TestCase(3)]
        public async Task UserService_UpdateAsync_Updates(int userId)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(userId))
                .ReturnsAsync(data.GetUserEntities[userId-1]);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await userAccountService.UpdateAsync(userId,data.GetUserModels[userId-1]);

            mockUnitOfWork.Verify(x => x.UserRepository.Update(It.IsAny<User>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserService_UpdateAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.UpdateAsync(data.GetUserModels[0].Id, data.GetUserModels[0]));
        }

        [TestCase(1)]
        public async Task UserService_UpdateAsync_ThrowsNicknameException(int id)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var model = data.GetUserModels[id - 1];
            model.Nickname = "nickname3";

            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(data.GetUserEntities[id-1]);

            mockUnitOfWork.Setup(m => m.UserRepository.IsNicknameTakenAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NicknameException>(() => userAccountService.UpdateAsync(1,model));
        }

        [Test]
        public async Task UserService_UpdateAsync_ThrowsDifferentEmailException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var model = data.GetUserModels[1];
            model.Nickname = "nickname3";

            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(data.GetUserEntities[0]);

            mockUnitOfWork.Setup(m => m.UserRepository.IsNicknameTakenAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<DifferenceEmailException>(() => userAccountService.UpdateAsync(1, model));
        }

        [TestCase(1,1)]
        public async Task UserService_ChangeRoleAsync_ChangesRole(int userId, int roleId)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.CredentialsRepository.Update(It.IsAny<Credentials>()));
            mockUnitOfWork.Setup(m => m.CredentialsRepository.GetByUserIdAsync(userId))
               .ReturnsAsync(data.GetAccountEntities.FirstOrDefault(x => x.UserId == userId));
            mockUnitOfWork.Setup(m => m.RoleRepository.GetByIdAsync(roleId))
                .ReturnsAsync(data.GetRoleEntities.FirstOrDefault(x => x.Id == roleId));

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await userAccountService.ChangeRoleAsync(data.GetAccountEntities[0].Id, 1);

            mockUnitOfWork.Verify(x => x.CredentialsRepository.Update(It.IsAny<Credentials>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }


        [TestCase(342, 1)]
        [TestCase(1, 3423)]
        public async Task UserService_ChangeRoleAsync_ThrowsNotFoundException(int userId, int roleId)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.CredentialsRepository.GetByUserIdAsync(userId))
                .ReturnsAsync(data.GetAccountEntities.FirstOrDefault(x => x.UserId == userId));
            mockUnitOfWork.Setup(m => m.RoleRepository.GetByIdAsync(roleId))
                .ReturnsAsync(data.GetRoleEntities.FirstOrDefault(x => x.Id == roleId));

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.ChangeRoleAsync(userId, roleId));
        }

        [Test]
        public async Task UserService_CreateRoleIfNotExistAsync_CreatessRole()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var role = new RoleModel { RoleName = "new rolellle" };

            mockUnitOfWork.Setup(m => m.RoleRepository.AddAsync(It.IsAny<Role>()));
            mockUnitOfWork.Setup(m => m.RoleRepository.GetAllAsync())
                .ReturnsAsync(data.GetRoleEntities);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await userAccountService.CreateRoleIfNotExist(role);

            mockUnitOfWork.Verify(x => x.RoleRepository.AddAsync(It.IsAny<Role>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserService_СreateRoleIfNotExistAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.RoleRepository.GetAllAsync())
                .ReturnsAsync(data.GetRoleEntities);        

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<AlreadyExistException>(() => userAccountService.CreateRoleIfNotExist(data.GetRoleModels[0]));
        }

        [Test]
        public async Task UserService_ChangeNicknameAsync_Changes()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetUserEntities[0]);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await userAccountService.ChangeNicknameAsync(data.GetUserEntities[0].Email, new NicknameRequest() { Nickname ="fdfdsfsdf"});

            mockUnitOfWork.Verify(x => x.UserRepository.Update(It.IsAny<User>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserService_ChangeNicknameAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.ChangeNicknameAsync("dsfsd", new NicknameRequest() { Nickname = "fdfdsfsdf" }));
        }

        [TestCase(true, "dsfdssdf")]
        [TestCase(false, "nickname2")]
        public async Task UserService_ChangeNicknameAsync_ThrowsNicknameException(bool isTaken, string nickname)
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetUserEntities[1]);

            mockUnitOfWork.Setup(m => m.UserRepository.IsNicknameTakenAsync(It.IsAny<string>()))
                .ReturnsAsync(isTaken);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NicknameException>(() => userAccountService.ChangeNicknameAsync(data.GetUserEntities[1].Email, new NicknameRequest() { Nickname = nickname }));
        }

        [Test]
        public async Task UserService_GetByRoleAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.RoleRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Role)null);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.GetByRoleAsync(It.IsAny<int>()));
        }
    }
}

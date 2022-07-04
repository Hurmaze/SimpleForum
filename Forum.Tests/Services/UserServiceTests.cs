using Services;
using Services.Models;
using Services.Services;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Services.Validation.Exceptions;
using DAL.Entities;

namespace Forum.Tests.Services
{
    internal class UserServiceTests
    {
        Data data;

        [Test]
        public async Task UserService_RegisterAsync_Registered()
        {
            data = new Data();

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
            data = new Data();

            var registerModel = new RegistrationModel { Email = "valid@email.com", Nickname = "SuperNIckname", Password = "Passw0rd", PasswordRepeat = "Passw0rd"};

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.IsEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<InvalidRegistrationException>(() => userAccountService.RegisterAsync(registerModel));
        }

        [TestCase(2)]
        public async Task UserService_DeleteByIdAsync_UserAccountDeleted(int id)
        {
            data = new Data();
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
            data = new Data();
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
            data = new Data();
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
            data = new Data();
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
            data = new Data();
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
            data = new Data();
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
            data = new Data();
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
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.RoleRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Role)null);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.DeleteRoleAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task UserService_UpdateAsync_Updates()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetUserEntities[0]);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await userAccountService.UpdateAsync(data.GetUserModels[0].Id,data.GetUserModels[0]);

            mockUnitOfWork.Verify(x => x.UserRepository.Update(It.IsAny<User>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserService_UpdateAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.UpdateAsync(data.GetUserModels[0].Id, data.GetUserModels[0]));
        }

        [Test]
        public async Task UserService_UpdateAsync_ThrowsNicknameTakenException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetUserEntities[1]);

            mockUnitOfWork.Setup(m => m.UserRepository.IsNicknameTakenAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NicknameTakenException>(() => userAccountService.UpdateAsync(data.GetUserModels[0].Id,data.GetUserModels[0]));
        }

        [Test]
        public async Task UserService_ChangeRoleAsync_ChangesRole()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.CredentialsRepository.Update(data.GetAccountEntities[0]));
            mockUnitOfWork.Setup(m => m.RoleRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(data.GetRoleEntities[0]);
            mockUnitOfWork.Setup(m => m.CredentialsRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(data.GetAccountEntities[0]);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await userAccountService.ChangeRoleAsync(data.GetAccountEntities[0].Id, 1);

            mockUnitOfWork.Verify(x => x.CredentialsRepository.Update(It.IsAny<Credentials>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }


        [TestCase(342, 1)]
        [TestCase("email1@gmail.com", 3423)]
        public async Task UserService_ChangeRoleAsync_ThrowsNotFoundException(int id, int roleId)
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.CredentialsRepository.GetByUserIdAsync(id))
                .ReturnsAsync(data.GetAccountEntities.FirstOrDefault(x => x.UserId == id));
            mockUnitOfWork.Setup(m => m.RoleRepository.GetByIdAsync(roleId))
                .ReturnsAsync(data.GetRoleEntities.FirstOrDefault(x => x.Id == roleId));

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.ChangeRoleAsync(id, roleId));
        }

        [Test]
        public async Task UserService_CreateRoleIfNotExistAsync_CreatessRole()
        {
            data = new Data();
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
            data = new Data();
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
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetUserEntities[0]);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            await userAccountService.ChangeNicknameAsync(data.GetUserEntities[0].Email, new NicknameModel() { Nickname ="fdfdsfsdf"});

            mockUnitOfWork.Verify(x => x.UserRepository.Update(It.IsAny<User>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserService_ChangeNicknameAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.ChangeNicknameAsync("dsfsd", new NicknameModel() { Nickname = "fdfdsfsdf" }));
        }

        [TestCase(true, "dsfdssdf")]
        [TestCase(false, "nickname2")]
        public async Task UserService_ChangeNicknameAsync_ThrowsNicknameTakenException(bool isTaken, string nickname)
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetUserEntities[1]);

            mockUnitOfWork.Setup(m => m.UserRepository.IsNicknameTakenAsync(It.IsAny<string>()))
                .ReturnsAsync(isTaken);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserService(mockUnitOfWork.Object, data.CreateMapperProfile(), mockLogger.Object);

            Assert.ThrowsAsync<NicknameTakenException>(() => userAccountService.ChangeNicknameAsync(data.GetUserEntities[1].Email, new NicknameModel() { Nickname = nickname }));
        }
    }
}

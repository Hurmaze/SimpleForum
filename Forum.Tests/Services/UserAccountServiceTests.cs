using Services;
using Services.Models;
using Services.Services;
using DAL.Entities.Account;
using DAL.Entities.Forum;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using Services.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Tests.Services
{
    internal class UserAccountServiceTests
    {
        Data data;
        private char[] password;

        [Test]
        public async Task UserAccountService_RegisterAsync_Registered()
        {
            data = new Data();

            var registerModel = new RegistrationModel { Email = "valid@email.com", Nickname = "SuperNIckname", Password = "Passw0rd", PasswordRepeat = "Passw0rd"};
            
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AccountRepository.AddAsync(It.IsAny<Account>()));
            mockUnitOfWork.Setup(m => m.UserRepository.AddAsync(It.IsAny<User>()));
            mockUnitOfWork.Setup(m => m.RoleRepository.GetAllAsync())
                .ReturnsAsync(data.GetRoleEntities);
            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            await userAccountService.RegisterAsync(registerModel);

            mockUnitOfWork.Verify(x => x.UserRepository.AddAsync(It.IsAny<User>()), Times.Once);
            mockUnitOfWork.Verify(x => x.AccountRepository.AddAsync(It.IsAny<Account>()), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public async Task UserAccountService_RegisterAsync_ThrowsInvalidRegistrationException()
        {
            data = new Data();

            var registerModel = new RegistrationModel { Email = "valid@email.com", Nickname = "SuperNIckname", Password = "Passw0rd", PasswordRepeat = "Passw0rd"};

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.AccountRepository.IsEmailExistAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<InvalidRegistrationException>(() => userAccountService.RegisterAsync(registerModel));
        }

        [TestCase(2)]
        public async Task UserAccountService_DeleteByIdAsync_UserAccountDeleted(int id)
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(id))
                .ReturnsAsync(data.GetUserEntities[id - 1]);

            mockUnitOfWork.Setup(m => m.UserRepository.DeleteByIdAsync(id));

            mockUnitOfWork.Setup(m => m.AccountRepository.DeleteByIdAsync(id));
                
            mockUnitOfWork.Setup(m => m.AccountRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetAccountEntities[0]);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            await userAccountService.DeleteByIdAsync(id);

            mockUnitOfWork.Verify(x => x.UserRepository.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(x => x.AccountRepository.DeleteByIdAsync(It.IsAny<int>()), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserAccountService_DeleteByIdAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.DeleteByIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task UserAccountService_GetAllAsync_ReturnAllUsers()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetAllAsync())
                .ReturnsAsync(data.GetUserEntities);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            var posts = await userAccountService.GetAllAsync();

            mockUnitOfWork.Verify(x => x.UserRepository.GetAllAsync(), Times.Once());
            Assert.NotNull(posts);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task UserAccountService_GetByIdAsync_ReturnUserModel(int id)
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(id))
                .ReturnsAsync(data.GetUserEntities[id - 1]);
            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            var post = await userAccountService.GetByIdAsync(id);

            mockUnitOfWork.Verify(x => x.UserRepository.GetByIdAsync(id), Times.Once());
            Assert.NotNull(post);
        }

        [Test]
        public async Task ForumThreadService_GetByIdAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.UserRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.GetByIdAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task UserAccountService_GetAllRolesAsync_ReturnAllRoles()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.RoleRepository.GetAllAsync())
                .ReturnsAsync(data.GetRoleEntities);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            var posts = await userAccountService.GetAllRolesAsync();

            mockUnitOfWork.Verify(x => x.RoleRepository.GetAllAsync(), Times.Once());
            Assert.NotNull(posts);
        }

        [TestCase(2)]
        public async Task UserAccountService_DeleteRoleAsync_RoleDeleted(int id)
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.RoleRepository.DeleteByIdAsync(id))
                .ReturnsAsync(data.GetRoleEntities[id -1]);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            await userAccountService.DeleteRoleAsync(id);

            mockUnitOfWork.Verify(x => x.RoleRepository.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserAccountService_DeleteRoleAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.RoleRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Role)null);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.DeleteRoleAsync(It.IsAny<int>()));
        }

        [Test]
        public async Task UserAccountService_UpdateAsync_Updates()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetUserEntities[0]);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            await userAccountService.UpdateAsync(data.GetUserModels[0]);

            mockUnitOfWork.Verify(x => x.UserRepository.Update(It.IsAny<User>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserAccountService_UpdateAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.UpdateAsync(data.GetUserModels[0]));
        }

        [Test]
        public async Task UserAccountService_UpdateAsync_ThrowsNicknameTakenException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetUserEntities[1]);

            mockUnitOfWork.Setup(m => m.UserRepository.IsNicknameTakenAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<NicknameTakenException>(() => userAccountService.UpdateAsync(data.GetUserModels[0]));
        }

        [Test]
        public async Task UserAccountService_ChangeRoleAsync_ChangesRole()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.AccountRepository.Update(data.GetAccountEntities[0]));
            mockUnitOfWork.Setup(m => m.RoleRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(data.GetRoleEntities[0]);
            mockUnitOfWork.Setup(m => m.AccountRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetAccountEntities[0]);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            await userAccountService.ChangeRoleAsync(data.GetUserModels[0].Email, 1);

            mockUnitOfWork.Verify(x => x.AccountRepository.Update(It.IsAny<Account>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }


        [TestCase("notExist@email", 1)]
        [TestCase("email1@gmail.com", 3423)]
        public async Task UserAccountService_ChangeRoleAsync_ThrowsNotFoundException(string email, int roleId)
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.AccountRepository.GetByEmailAsync(email))
                .ReturnsAsync(data.GetAccountEntities.FirstOrDefault(x => x.Email == email));
            mockUnitOfWork.Setup(m => m.RoleRepository.GetByIdAsync(roleId))
                .ReturnsAsync(data.GetRoleEntities.FirstOrDefault(x => x.Id == roleId));

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.ChangeRoleAsync(email, roleId));
        }

        [Test]
        public async Task UserAccountService_CreateRoleIfNotExistAsync_CreatessRole()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var role = new RoleModel { RoleName = "new rolellle" };

            mockUnitOfWork.Setup(m => m.RoleRepository.AddAsync(It.IsAny<Role>()));
            mockUnitOfWork.Setup(m => m.RoleRepository.GetAllAsync())
                .ReturnsAsync(data.GetRoleEntities);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            await userAccountService.CreateRoleIfNotExist(role);

            mockUnitOfWork.Verify(x => x.RoleRepository.AddAsync(It.IsAny<Role>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserAccountService_СreateRoleIfNotExistAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.RoleRepository.GetAllAsync())
                .ReturnsAsync(data.GetRoleEntities);        

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<AlreadyExistException>(() => userAccountService.CreateRoleIfNotExist(data.GetRoleModels[0]));
        }

        [Test]
        public async Task UserAccountservice_LoginAsync_logins()
        {
            data = new Data();
            byte[] passwordHash;
            byte[] passwordSalt;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Passw0rd"));
            }
            var account = new Account 
            { 
                Email = "valid@email.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = data.GetRoleEntities[0]
            };
            var loginModel = new LoginModel 
            { 
                Email = "valid@email.com", 
                Password = "Passw0rd" 
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.AccountRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(account);

            var mockLogger = new Mock<ILogger<UserAccountService>>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKeydsfsdf342"));
            var jwtoptions = new Mock<IOptions<JwtOptions>>();
            jwtoptions.Setup(j => j.Value)
                .Returns(new JwtOptions() {Secret = "superSecretKeydsfsdf342" });

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            string token = await userAccountService.LoginAsync(loginModel);
            Assert.IsNotNull(token);
            Assert.IsNotEmpty(token);
        }

        [Test]
        public async Task UserAccountService_ChangeNicknameAsync_Changes()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetUserEntities[0]);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            await userAccountService.ChangeNicknameAsync(data.GetUserEntities[0].Email, new NicknameModel() { Nickname ="fdfdsfsdf"});

            mockUnitOfWork.Verify(x => x.UserRepository.Update(It.IsAny<User>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
        }

        [Test]
        public async Task UserAccountService_ChangeNicknameAsync_ThrowsNotFoundException()
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.ChangeNicknameAsync("dsfsd", new NicknameModel() { Nickname = "fdfdsfsdf" }));
        }

        [TestCase(true, "dsfdssdf")]
        [TestCase(false, "nickname2")]
        public async Task UserAccountService_ChangeNicknameAsync_ThrowsNicknameTakenException(bool isTaken, string nickname)
        {
            data = new Data();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(data.GetUserEntities[1]);

            mockUnitOfWork.Setup(m => m.UserRepository.IsNicknameTakenAsync(It.IsAny<string>()))
                .ReturnsAsync(isTaken);

            var mockLogger = new Mock<ILogger<UserAccountService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new UserAccountService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<NicknameTakenException>(() => userAccountService.ChangeNicknameAsync(data.GetUserEntities[1].Email, new NicknameModel() { Nickname = nickname }));
        }
    }
}

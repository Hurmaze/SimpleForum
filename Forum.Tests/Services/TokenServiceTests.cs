using DAL.Entities;
using DAL.Interfaces;
using Forum.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using Services;
using Services.Models;
using Services.Services;
using Services.Validation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Tests.Services
{
    public class TokenServiceTests
    {
        private ServiceHelper data;

        [TestCase("email@gmail.com","Passw0rd", "email@gmail.com", "Passw0rd")]
        [TestCase("email2222@gmail.com","SuperPass0", "email2222@gmail.com", "SuperPass0")]
        public async Task TokenService_GetTokenAsync_logins(string userEmail, string userPassword,string loginEmail, string loginPassword)
        {
            data = new ServiceHelper();
            byte[] passwordHash;
            byte[] passwordSalt;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userPassword));
            }
            var user = new User
            {
                Email = userEmail,
                Credentials = new Credentials
                {
                    UserId = 1,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = data.GetRoleEntities[0]
                }
            };

            var loginModel = new LoginRequest
            {
                Email = loginEmail,
                Password = loginPassword
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            var mockLogger = new Mock<ILogger<UserService>>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKeydsfsdf342"));
            var jwtoptions = new Mock<IOptions<JwtOptions>>();
            jwtoptions.Setup(j => j.Value)
                .Returns(new JwtOptions() { Secret = "superSecretKeydsfsdf342" });

            var userAccountService = new TokenService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            string token = await userAccountService.GetTokenAsync(loginModel);
            Assert.IsNotNull(token);
            Assert.IsNotEmpty(token);
        }

        [TestCase("email@gmail.com", "Passw0rd", "email@gmail.com", "Passw0rddddd")]
        [TestCase("email@gmail.com", "SuperPass0deeqasd", "email@gmail.com", "SuperPass0")]
        public async Task TokenService_GetTokenAsync_ThrowsWrongPasswordException(string userEmail, string userPassword, string loginEmail, string loginPassword)
        {
            data = new ServiceHelper();
            byte[] passwordHash;
            byte[] passwordSalt;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userPassword));
            }
            var user = new User
            {
                Email = "valid@email.com",
                Credentials = new Credentials
                {
                    UserId = 1,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = data.GetRoleEntities[0]
                }
            };

            var loginModel = new LoginRequest
            {
                Email = "valid@email.com",
                Password = loginPassword
            };


            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            var mockLogger = new Mock<ILogger<UserService>>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKeydsfsdf342"));
            var jwtoptions = new Mock<IOptions<JwtOptions>>();
            jwtoptions.Setup(j => j.Value)
                .Returns(new JwtOptions() { Secret = "superSecretKeydsfsdf342" });

            var userAccountService = new TokenService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<WrongPasswordException>(() => userAccountService.GetTokenAsync(loginModel));
        }

        [Test]
        public async Task TokenService_GetTokenAsync_ThrowsNotFoundException()
        {
            data = new ServiceHelper();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.UserRepository.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            var mockLogger = new Mock<ILogger<UserService>>();
            var jwtoptions = new Mock<IOptions<JwtOptions>>();

            var userAccountService = new TokenService(mockUnitOfWork.Object, data.CreateMapperProfile(), jwtoptions.Object, mockLogger.Object);

            Assert.ThrowsAsync<NotFoundException>(() => userAccountService.GetTokenAsync(new LoginRequest()));
        }
    }
}

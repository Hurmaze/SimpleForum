using DAL.Entities;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using Services;
using Services.Models;
using Services.Services;
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
        private Data data;

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
                Password = "Passw0rd"
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
    }
}

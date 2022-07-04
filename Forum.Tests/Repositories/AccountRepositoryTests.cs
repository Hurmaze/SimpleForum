using DAL.DbAccess;
using DAL.Entities;
using DAL.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Tests.Repositories
{
    internal class CredentialsRepositoryTests
    {
        static List<Role> roles = new List<Role>
            {
                new Role { Id = 1, RoleName = "user"},
                new Role { Id = 2, RoleName = "admin"}
            };

        List<Credentials> expectedAccounts = new List<Credentials>
            {
                new Credentials { Id = 1, Role = roles[0], UserId = 1, PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Id = 2, Role = roles[0], UserId = 2, PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Id = 3, Role = roles[0], UserId = 3, PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Id = 4, Role = roles[1], UserId = 4, PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Id = 5, Role = roles[1], UserId = 5, PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }}
            };

        [TestCase(1)]
        [TestCase(4)]
        public async Task CredentialsRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var credentialsRepository = new CredentialsRepository(context);

            var account = await credentialsRepository.GetByIdAsync(id);

            var expected = expectedAccounts.FirstOrDefault(x => x.Id == id);

            Assert.That(account, Is.EqualTo(expected).Using(new AccountEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task credentialsRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var credentialsRepository = new CredentialsRepository(context);
            var accounts = await credentialsRepository.GetAllAsync();
            accounts = accounts.OrderBy(x => x.Id);

            Assert.That(accounts, Is.EqualTo(expectedAccounts).Using(new AccountEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task CredentialsRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var credentialsRepository = new CredentialsRepository(context);
            var account = new Credentials {
                UserId = 6,
                PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            await credentialsRepository.AddAsync(account);
            await context.SaveChangesAsync();

            Assert.That(context.Credentials.Count(), Is.EqualTo(expectedAccounts.Count + 1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task CredentialsRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var credentialsRepository = new CredentialsRepository(context);

            await credentialsRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Credentials.Count(), Is.EqualTo(expectedAccounts.Count - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public void CredentialsRepository_Update_TimesOnce()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var credentialsRepository = new CredentialsRepository(context);
            var theme = new Credentials { Id = 1, PasswordHash = new byte[] { 0x20, 0x20, 0x10, 0x20, 0x20, 0x20, 0x20 } };

            Assert.DoesNotThrow(() => credentialsRepository.Update(theme));
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task CredentialsRepository_GetByUserIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var credentialsRepository = new CredentialsRepository(context);

            var account = await credentialsRepository.GetByUserIdAsync(id);

            var expected = expectedAccounts.FirstOrDefault(x => x.Id == id);

            Assert.That(account, Is.EqualTo(expected).Using(new AccountEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }
    }
}

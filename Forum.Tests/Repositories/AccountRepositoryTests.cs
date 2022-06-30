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
    internal class AccountRepositoryTests
    {
        static List<Role> roles = new List<Role>
            {
                new Role { Id = 1, RoleName = "user"},
                new Role { Id = 2, RoleName = "admin"}
            };

        List<Credentials> expectedAccounts = new List<Credentials>
            {
                new Credentials { Id = 1, Role = roles[0], Email = "email1@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Id = 2, Role = roles[0], Email = "email2@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Id = 3, Role = roles[0], Email = "email3@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Id = 4, Role = roles[1], Email = "email4@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Credentials { Id = 5, Role = roles[1], Email = "email5@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }}
            };

        [TestCase(1)]
        [TestCase(4)]
        public async Task AccountRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var accountRepository = new AccountRepository(context);

            var account = await accountRepository.GetByIdAsync(id);

            var expected = expectedAccounts.FirstOrDefault(x => x.Id == id);

            Assert.That(account, Is.EqualTo(expected).Using(new AccountEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task accountRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var accountRepository = new AccountRepository(context);
            var accounts = await accountRepository.GetAllAsync();
            accounts = accounts.OrderBy(x => x.Id);

            Assert.That(accounts, Is.EqualTo(expectedAccounts).Using(new AccountEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task accountRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var accountRepository = new AccountRepository(context);
            var account = new Credentials {
                Email = "email6@gmail.com",
                PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }
            };

            await accountRepository.AddAsync(account);
            await context.SaveChangesAsync();

            Assert.That(context.Accounts.Count(), Is.EqualTo(expectedAccounts.Count + 1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task accountRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var accountRepository = new AccountRepository(context);

            await accountRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Accounts.Count(), Is.EqualTo(expectedAccounts.Count - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public void ThemeRepository_Update_TimesOnce()
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var accountRepository = new AccountRepository(context);
            var theme = new Credentials { Id = 1, PasswordHash = new byte[] { 0x20, 0x20, 0x10, 0x20, 0x20, 0x20, 0x20 } };

            Assert.DoesNotThrow(() => accountRepository.Update(theme));
        }

        [Test]
        public async Task AccountRepository_IsEmailExist_ReturnTrue()
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var accountRepository = new AccountRepository(context);

            Assert.IsTrue(await accountRepository.IsEmailExistAsync(expectedAccounts[0].Email));
        }

        [Test]
        public async Task AccountRepository_IsEmailExist_ReturnFalse()
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var accountRepository = new AccountRepository(context);

            Assert.IsFalse(await accountRepository.IsEmailExistAsync("notexist@gmail.com"));
        }

        [TestCase("email1@gmail.com")]
        [TestCase("email4@gmail.com")]
        public async Task AccountRepository_GetByEmailAsync_ReturnsSingleValue(string email)
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var accountRepository = new AccountRepository(context);

            var account = await accountRepository.GetByEmailAsync(email);

            var expected = expectedAccounts.FirstOrDefault(x => x.Email == email);

            Assert.That(account, Is.EqualTo(expected).Using(new AccountEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }
    }
}

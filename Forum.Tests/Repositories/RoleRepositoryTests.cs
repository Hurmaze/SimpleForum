using DAL.DbAccess;
using DAL.Entities.Account;
using DAL.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Tests.Repositories
{
    internal class RoleRepositoryTests
    {
        List<Role> expectedRoles = new List<Role>
            {
                new Role { Id = 1, RoleName = "user"},
                new Role { Id = 2, RoleName = "admin"}
            };

        [TestCase(1)]
        [TestCase(2)]
        public async Task RoleRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var roleRepository = new RoleRepository(context);

            var role = await roleRepository.GetByIdAsync(id);

            var expected = expectedRoles.FirstOrDefault(x => x.Id == id);

            Assert.That(role, Is.EqualTo(expected).Using(new RoleEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task RoleRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var roleRepository = new RoleRepository(context);
            var roles = await roleRepository.GetAllAsync();
            roles = roles.OrderBy(x => x.Id);

            Assert.That(roles, Is.EqualTo(expectedRoles).Using(new RoleEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task RoleRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var roleRepository = new RoleRepository(context);
            var role = new Role { RoleName = "Clerk" };

            await roleRepository.AddAsync(role);
            await context.SaveChangesAsync();

            Assert.That(context.Roles.Count(), Is.EqualTo(expectedRoles.Count + 1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task RoleRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var roleRepository = new RoleRepository(context);

            await roleRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Roles.Count(), Is.EqualTo(expectedRoles.Count - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public void ThemeRepository_Update_TimesOnce()
        {
            using var context = new AccountDbContext(DataSeeder.GetAccountDbOptions());

            var roleRepository = new RoleRepository(context);
            var role = new Role { Id = 1, RoleName = "NewName" };

            Assert.DoesNotThrow(() => roleRepository.Update(role));

        }
    }
}

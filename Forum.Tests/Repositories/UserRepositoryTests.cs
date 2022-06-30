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
    internal class UserRepositoryTests
    {
        List<User> expectedUsers = new List<User>
        {
            new User { Id = 1, Email = "email1@gmail.com", Nickname = "nickname1" },
            new User { Id = 2, Email = "email2@gmail.com", Nickname = "nickname2" },
            new User { Id = 3, Email = "email3@gmail.com", Nickname = "nickname3" },
            new User { Id = 4, Email = "email4@gmail.com", Nickname = "nickname4" },
            new User { Id = 5, Email = "email5@gmail.com", Nickname = "nickname5" }
        };


        [TestCase(1)]
        [TestCase(4)]
        public async Task UserRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var userRepository = new UserRepository(context);

            var user = await userRepository.GetByIdAsync(id);

            var expected = expectedUsers.FirstOrDefault(x => x.Id == id);

            Assert.That(user, Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var userRepository = new UserRepository(context);
            var users = await userRepository.GetAllAsync();
            users = users.OrderBy(x => x.Id);

            Assert.That(users, Is.EqualTo(expectedUsers).Using(new UserEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var userRepository = new UserRepository(context);
            var user = new User
            {
                Email = "email6@gmail.com"
            };

            await userRepository.AddAsync(user);
            await context.SaveChangesAsync();

            Assert.That(context.Users.Count(), Is.EqualTo(expectedUsers.Count + 1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task UserRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var userRepository = new UserRepository(context);

            await userRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Users.Count(), Is.EqualTo(expectedUsers.Count - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public void UserRepository_Update_TimesOnce()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var userRepository = new UserRepository(context);
            var user = expectedUsers[0];
            user.Nickname = "New nick";

            Assert.DoesNotThrow(() => userRepository.Update(user));
        }

        [Test]
        public async Task UserRepository_IsEmailExist_ReturnTrue()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var userRepository = new UserRepository(context);

            Assert.IsTrue(await userRepository.IsNicknameTakenAsync(expectedUsers[0].Nickname));
        }

        [Test]
        public async Task UserRepository_IsEmailExist_ReturnFalse()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var userRepository = new UserRepository(context);

            Assert.IsFalse(await userRepository.IsNicknameTakenAsync("notexist"));
        }

        [TestCase("email1@gmail.com")]
        [TestCase("email4@gmail.com")]
        public async Task UserRepository_GetByEmailAsync_ReturnsSingleValue(string email)
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var userRepository = new UserRepository(context);

            var user = await userRepository.GetByEmailAsync(email);

            var expected = expectedUsers.FirstOrDefault(x => x.Email == email);

            Assert.That(user, Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }
    }
}

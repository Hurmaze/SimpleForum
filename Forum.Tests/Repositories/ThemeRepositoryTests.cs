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
    public class ThemeRepositoryTests
    {
        List<Theme> expectedThemes = new List<Theme>
        {
            new Theme { Id = 1, ThemeName = "Books" },
            new Theme { Id = 2, ThemeName = "Elephants" }
        };

        [TestCase(1)]
        [TestCase(2)]
        public async Task ThemeRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var themeRepository = new ThemeRepository(context);

            var theme = await themeRepository.GetByIdAsync(id);

            var expected = expectedThemes.FirstOrDefault(x => x.Id == id);

            Assert.That(theme, Is.EqualTo(expected).Using(new ThemeEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task ThemeRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var themeRepository = new ThemeRepository(context);
            var themes = await themeRepository.GetAllAsync();
            themes = themes.OrderBy(x => x.Id);

            Assert.That(themes, Is.EqualTo(expectedThemes).Using(new ThemeEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task ThemeRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var themeRepository = new ThemeRepository(context);
            var theme = new Theme { ThemeName = "Bananas" };

            await themeRepository.AddAsync(theme);
            await context.SaveChangesAsync();

            Assert.That(context.Themes.Count(), Is.EqualTo(expectedThemes.Count + 1), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task ThemeRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var themeRepository = new ThemeRepository(context);

            await themeRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Themes.Count(), Is.EqualTo(expectedThemes.Count - 1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public void ThemeRepository_Update_TimesOnce()
        {
            using var context = new ForumDbContext(DataSeeder.GetForumDbOptions());

            var themeRepository = new ThemeRepository(context);
            var theme = new Theme { Id = 1, ThemeName = "NewName" };

            Assert.DoesNotThrow(() => themeRepository.Update(theme));

        }
    }
}


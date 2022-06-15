using DAL.Entities.Account;
using DAL.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Tests
{
    internal class ThemeEqualityComparer : IEqualityComparer<Theme>
    {
        public bool Equals([AllowNull] Theme x, [AllowNull] Theme y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.ThemeName == y.ThemeName;
        }

        public int GetHashCode([DisallowNull] Theme obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class PostEqualityComparer : IEqualityComparer<Post>
    {
        public bool Equals([AllowNull] Post x, [AllowNull] Post y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Content == y.Content
                && x.Author.Email == y.Author.Email;
        }

        public int GetHashCode([DisallowNull] Post obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals([AllowNull] User x, [AllowNull] User y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Email == y.Email;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class ForumThreadEqualityComparer : IEqualityComparer<ForumThread>
    {
        public bool Equals([AllowNull] ForumThread x, [AllowNull] ForumThread y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Theme.ThemeName == y.Theme.ThemeName
                && x.Title == y.Title;
        }

        public int GetHashCode([DisallowNull] ForumThread obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class RoleEqualityComparer : IEqualityComparer<Role>
    {
        public bool Equals([AllowNull] Role x, [AllowNull] Role y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.RoleName == y.RoleName;
        }

        public int GetHashCode([DisallowNull] Role obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class AccountEqualityComparer : IEqualityComparer<Account>
    {
        public bool Equals([AllowNull] Account x, [AllowNull] Account y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Email == y.Email;
        }

        public int GetHashCode([DisallowNull] Account obj)
        {
            return obj.GetHashCode();
        }
    }
}

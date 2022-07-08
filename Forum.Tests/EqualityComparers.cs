using Services.Models;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities;

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

    internal class AccountEqualityComparer : IEqualityComparer<Credentials>
    {
        public bool Equals([AllowNull] Credentials x, [AllowNull] Credentials y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.UserId == y.UserId
                && x.RoleId == y.RoleId;
        }

        public int GetHashCode([DisallowNull] Credentials obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class ForumThreadModelEqualityComparer : IEqualityComparer<ForumThreadModel>
    {
        public bool Equals([AllowNull] ForumThreadModel x, [AllowNull] ForumThreadModel y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.AuthorId == y.AuthorId;
        }

        public int GetHashCode([DisallowNull] ForumThreadModel obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class UserModelEqualityComparer : IEqualityComparer<UserModel>
    {
        public bool Equals([AllowNull] UserModel x, [AllowNull] UserModel y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.CredentialsId == y.CredentialsId;
        }

        public int GetHashCode([DisallowNull] UserModel obj)
        {
            return obj.GetHashCode();
        }
    }
}

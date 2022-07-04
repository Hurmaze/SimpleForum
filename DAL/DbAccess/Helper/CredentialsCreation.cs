using DAL.Entities;
using System.Security.Cryptography;
using System.Text;

namespace DAL.DbAccess.Helper
{
    /// <summary>
    /// Helper to seed data into AccountDbContext
    /// </summary>
    internal class CredentialsCreation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal static Credentials CreateCredentials(Credentials credentials, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            Credentials newaccount = new Credentials
            {
                Id = credentials.Id,
                UserId = credentials.UserId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = credentials.RoleId
            };

            return newaccount;
        }
    }
}

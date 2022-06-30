using DAL.Entities;
using DAL.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbAccess.Helper
{
    /// <summary>
    /// Helper to seed data into AccountDbContext
    /// </summary>
    internal class AccountCreation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal static Credentials CreateAccount(Credentials account, string password)
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
                Id = account.Id,
                Email = account.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = account.RoleId
            };

            return newaccount;
        }
    }
}

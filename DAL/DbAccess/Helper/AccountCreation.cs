﻿using DAL.Entities.Account;
using DAL.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbAccess.Helper
{
    internal class AccountCreation
    {
        internal static Account CreateAccount(Account account, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            Account newaccount = new Account
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
﻿using DAL.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<bool> IsNicknameTaken(string nickname);
    }
}
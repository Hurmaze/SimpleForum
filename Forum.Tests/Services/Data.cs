using AutoMapper;
using Services;
using Services.Models;
using DAL.Entities.Account;
using DAL.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Tests.Services
{
    internal class Data
    {
        private readonly IMapper _mapper;

        public Data()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            _mapper = new Mapper(configuration);
        }
        public List<User> GetUserEntities =>
            new List<User>
            {
                new User { Id = 1, Email = "email1@gmail.com", Nickname = "nickname1" },
                new User { Id = 2, Email = "email2@gmail.com", Nickname = "nickname2" },
                new User { Id = 3, Email = "email3@gmail.com", Nickname = "nickname3" },
                new User { Id = 4, Email = "email4@gmail.com", Nickname = "nickname4" },
                new User { Id = 5, Email = "email5@gmail.com", Nickname = "nickname5" }
            };

        public List<Theme> GetThemeEntities =>
            new List<Theme>
            {
                new Theme { Id = 1, ThemeName = "Books" },
                new Theme { Id = 2, ThemeName = "Elephants" }
            };

        public List<ForumThread> GetForumThreadEntities =>
            new List<ForumThread>
            {
                new ForumThread { Id = 1, Author = GetUserEntities[0], Content = "Some text", Title = "Super elephants", Theme = GetThemeEntities[1], TimeCreated = DateTime.Parse("2022-01-02") },
                new ForumThread { Id = 2, Author = GetUserEntities[3], Content = "My first book was...", Title = "Man I love books", Theme = GetThemeEntities[0], TimeCreated = DateTime.Now }
            };

        public List<Post> GetPostEntities =>
            new List<Post>
            {
                new Post { Id = 1, Thread = GetForumThreadEntities[0], Content = "Man i love elephants!", Author = GetUserEntities[1], TimeCreated = DateTime.Now },
                new Post { Id = 2, Thread = GetForumThreadEntities[0], Content = "My favourite elephant is...", Author = GetUserEntities[2], TimeCreated = DateTime.Now},
                new Post { Id = 3, Thread = GetForumThreadEntities[1], Content = "Books are great you know.", Author = GetUserEntities[4], TimeCreated = DateTime.Now},
                new Post { Id= 4, Thread = GetForumThreadEntities[1], Content = "Read recently about Segriy Zhadan... He is cool.", Author = GetUserEntities[0], TimeCreated = DateTime.Now}
            };

        public List<Role> GetRoleEntities =>
            new List<Role>
            {
                new Role { Id = 1, RoleName = "user"},
                new Role { Id = 2, RoleName = "admin"}
            };

        public List<Account> GetAccountEntities =>
            new List<Account>
            {
                new Account { Id = 1, Role = GetRoleEntities[0], Email = "email1@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Account { Id = 2, Role = GetRoleEntities[0], Email = "email2@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Account { Id = 3, Role = GetRoleEntities[0], Email = "email3@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Account { Id = 4, Role = GetRoleEntities[1], Email = "email4@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }},
                new Account { Id = 5, Role = GetRoleEntities[1], Email = "email5@gmail.com", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }}
            };
        public List<UserModel> GetUserModels => _mapper.Map<List<UserModel>>(GetUserEntities);

        public List<ThemeModel> GetThemeModels => _mapper.Map<List<ThemeModel>>(GetThemeEntities);

        public List<ForumThreadModel> GetForumThreadModels => _mapper.Map<List<ForumThreadModel>>(GetForumThreadEntities);

        public List<PostModel> GetPostModels => _mapper.Map<List<PostModel>>(GetPostEntities);

        public List<RoleModel> GetRoleModels => _mapper.Map<List<RoleModel>>(GetRoleEntities);

        public List<AccountModel> GetAccountModels => _mapper.Map<List<AccountModel>>(GetAccountEntities);

        public IMapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }



    }
}

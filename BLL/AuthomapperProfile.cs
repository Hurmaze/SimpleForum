using AutoMapper;
using BLL.Models;
using DAL.Entities.Account;
using DAL.Entities.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(um => um.PostsIds, x => x.MapFrom(u => u.ThreadPosts.Select(y => y.Id)))
                .ForMember(um => um.PostsIds, x => x.MapFrom(u => u.Threads.Select(y => y.Id)))
                .ReverseMap();

            CreateMap<User, RegistrationModel>()
                .ForMember(um => um.PostsIds, x => x.MapFrom(u => u.ThreadPosts.Select(y => y.Id)))
                .ForMember(um => um.PostsIds, x => x.MapFrom(u => u.Threads.Select(y => y.Id)))
                .ReverseMap();

            CreateMap<Account, AccountModel>()
                .ForMember(um => um.RoleName, x => x.MapFrom(a => a.Role.RoleName))
                .ReverseMap();

           
            CreateMap<Post, PostModel>()
                .ForMember(pm => pm.ThreadId, x => x.MapFrom(p => p.Thread.Id))
                .ForMember(pm => pm.AuthorId, x => x.MapFrom(p => p.Author.Id))
                .ReverseMap();

            CreateMap<ForumThread, ForumThreadModel>()
                .ForMember(fm => fm.ThemeName, x => x.MapFrom(p => p.Theme.ThemeName))
                .ForMember(pm => pm.AuthorId, x => x.MapFrom(p => p.Author.Id))
                .ForMember(pm => pm.ThreadPostsIds, x => x.MapFrom(p => p.ThreadPosts.Select(y => y.Id)))
                .ReverseMap();

            CreateMap<Theme, ThemeModel>()
                .ForMember(tm => tm.ForumThreads, x => x.MapFrom(t => t.ForumThreads.Select(y => y.Id)))
                .ReverseMap();

            CreateMap<Role, RoleModel>()
                .ForMember(rm => rm.AccountAuthsIds, x => x.MapFrom(r => r.Accounts.Select(a => a.Id)))
                .ReverseMap();
        }
    }
}

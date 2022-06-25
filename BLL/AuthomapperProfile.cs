using AutoMapper;
using Services.Models;
using DAL.Entities.Account;
using DAL.Entities.Forum;

namespace Services
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(um => um.PostsIds, x => x.MapFrom(u => u.ThreadPosts.Select(y => y.Id)))
                .ForMember(um => um.ThreadsIds, x => x.MapFrom(u => u.Threads.Select(y => y.Id)))
                .ReverseMap();

            CreateMap<User, RegistrationModel>()
                .ReverseMap();

            CreateMap<Account, AccountModel>()
                .ForMember(um => um.RoleName, x => x.MapFrom(a => a.Role.RoleName))
                .ReverseMap();


            CreateMap<Post, PostModel>()
                .ForMember(pm => pm.ThreadId, x => x.MapFrom(p => p.Thread.Id))
                .ForMember(pm => pm.AuthorId, x => x.MapFrom(p => p.Author.Id))
                .ForMember(pm => pm.TimeCreated, x => x.MapFrom(p => p.TimeCreated))
                .ForMember(pm => pm.AuthorEmail, x => x.MapFrom(p => p.Author.Email))
                .ForMember(pm => pm.AuthorNickname, x => x.MapFrom(p => p.Author.Nickname));

            CreateMap<PostModel, Post>();

            CreateMap<ForumThread, ForumThreadModel>()
                .ForMember(fm => fm.ThemeName, x => x.MapFrom(p => p.Theme.ThemeName))
                .ForMember(pm => pm.AuthorId, x => x.MapFrom(p => p.Author.Id))
                .ForMember(pm => pm.AuthorEmail, x => x.MapFrom(p => p.Author.Email))
                .ForMember(pm => pm.AuthorNickname, x => x.MapFrom(p => p.Author.Nickname))
                .ForMember(pm => pm.ThreadPostsIds, x => x.MapFrom(p => p.ThreadPosts.Select(y => y.Id)));

            CreateMap<ForumThreadModel, ForumThread>();

            CreateMap<Theme, ThemeModel>()
                .ForMember(tm => tm.ForumThreads, x => x.MapFrom(t => t.ForumThreads.Select(y => y.Id)))
                .ReverseMap();

            CreateMap<Role, RoleModel>()
                .ForMember(rm => rm.AccountAuthsIds, x => x.MapFrom(r => r.Accounts.Select(a => a.Id)))
                .ReverseMap();
        }
    }
}

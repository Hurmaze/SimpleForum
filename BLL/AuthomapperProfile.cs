using AutoMapper;
using Services.Models;
using DAL.Entities.Account;
using DAL.Entities.Forum;

namespace Services
{
    /// <summary>
    /// Auttomapper profile.
    /// </summary>
    /// <seealso cref="Profile" />
    public class AutomapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutomapperProfile"/> class.
        /// </summary>
        public AutomapperProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(um => um.PostsIds, x => x.MapFrom(u => u.ThreadPosts.Select(y => y.Id)))
                .ForMember(um => um.ThreadsIds, x => x.MapFrom(u => u.Threads.Select(y => y.Id)))
                .ReverseMap();

            CreateMap<Account, UserModel>()
                .ForMember(um => um.RoleName, x => x.MapFrom(u => u.Role.RoleName));

            CreateMap<Tuple<User, Account>, UserModel>()
                .ForMember(um => um.PostsIds, x => x.MapFrom(u => u.Item1.ThreadPosts.Select(y => y.Id)))
                .ForMember(um => um.ThreadsIds, x => x.MapFrom(u => u.Item1.Threads.Select(y => y.Id)))
                .ForMember(um => um.RoleName, x => x.MapFrom(u => u.Item2.Role.RoleName))
                .ForMember(um => um.Email, x => x.MapFrom(u => u.Item1.Email))
                .ForMember(um => um.RoleId, x => x.MapFrom(u => u.Item2.RoleId))
                .ForMember(um => um.Nickname, x => x.MapFrom(u => u.Item1.Nickname))
                .ForMember(um => um.Id, x => x.MapFrom(u => u.Item1.Id));



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
                .ForMember(tm => tm.ForumThreads, x => x.MapFrom(t => t.ForumThreads.Select(y => y.Id)));

            CreateMap<ThemeModel, Theme>()
                .ForMember(t => t.ThemeName, x => x.MapFrom(tm => tm.ThemeName));

            CreateMap<Role, RoleModel>()
                .ForMember(rm => rm.AccountAuthsIds, x => x.MapFrom(r => r.Accounts.Select(a => a.Id)))
                .ReverseMap();
        }
    }
}

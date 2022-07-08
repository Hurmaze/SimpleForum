using AutoMapper;
using Services.Models;
using DAL.Entities;

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
                .ForMember(um => um.RoleName, x => x.MapFrom(u => u.Credentials.Role.RoleName))
                .ReverseMap();

            CreateMap<Post, PostModel>()
                .ForMember(pm => pm.ThreadId, x => x.MapFrom(p => p.Thread.Id))
                .ForMember(pm => pm.AuthorId, x => x.MapFrom(p => p.Author.Id))
                .ForMember(pm => pm.TimeCreated, x => x.MapFrom(p => p.TimeCreated))
                .ForMember(pm => pm.AuthorEmail, x => x.MapFrom(p => p.Author.Email))
                .ForMember(pm => pm.AuthorNickname, x => x.MapFrom(p => p.Author.Nickname));

            CreateMap<PostRequest, Post>();

            CreateMap<ForumThread, ForumThreadModel>()
                .ForMember(fm => fm.ThemeName, x => x.MapFrom(p => p.Theme.ThemeName))
                .ForMember(pm => pm.AuthorId, x => x.MapFrom(p => p.Author.Id))
                .ForMember(pm => pm.AuthorEmail, x => x.MapFrom(p => p.Author.Email))
                .ForMember(pm => pm.AuthorNickname, x => x.MapFrom(p => p.Author.Nickname));

            CreateMap<ForumThreadRequest, ForumThread>();

            CreateMap<Theme, ThemeModel>().ReverseMap();

            CreateMap<Role, RoleModel>().ReverseMap();
        }
    }
}

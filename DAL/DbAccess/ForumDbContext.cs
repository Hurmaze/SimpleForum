using DAL.Entities.Forum;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbAccess
{
    public class ForumDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ForumThread> Threads { get; set; }
        public DbSet<Theme> Themes { get; set; }

        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FluentApi
            var user = modelBuilder.Entity<User>();
            user.HasMany(a => a.ThreadPosts).WithOne(t => t.Author)
                .OnDelete(DeleteBehavior.ClientSetNull);
            user.HasMany(a => a.Threads).WithOne(t => t.Author)
                .OnDelete(DeleteBehavior.ClientSetNull);
            user.HasIndex(a => a.Email).IsUnique();
            user.Property(a => a.Email).HasMaxLength(100).IsRequired();
            user.Property(a => a.Nickname).HasMaxLength(30);
            user.ToTable("Users");

            var thread = modelBuilder.Entity<ForumThread>();
            thread.ToTable("Threads");
            thread.HasOne(a => a.Theme).WithMany(t => t.ForumThreads)
                .OnDelete(DeleteBehavior.ClientSetNull);
            thread.HasMany(p => p.ThreadPosts).WithOne(c => c.Thread);
            thread.Property(p => p.Title).HasMaxLength(100);
            thread.Property(p => p.TimeCreated).HasDefaultValueSql("GETDATE()");

            var theme = modelBuilder.Entity<Theme>();
            theme.Property(th => th.ThemeName).HasMaxLength(50).IsRequired();

            var post = modelBuilder.Entity<Post>();
            post.Property(p => p.TimeCreated).HasDefaultValueSql("GETDATE()");
            post.ToTable("ThreadPosts");
            #endregion

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Email = "user1@gmail.com", Nickname = "user1" },
                    new User { Id = 2, Email = "user2@gmail.com", Nickname = "user2" },
                    new User { Id = 3, Email = "user3@gmail.com", Nickname = "user3" },
                    new User { Id = 4, Email = "moderator1@gmail.com", Nickname = "moderator1" },
                    new User { Id = 5, Email = "admin1@gmail.com", Nickname = "admin1" });

            modelBuilder.Entity<Theme>().HasData(
                    new Theme { Id = 1, ThemeName = "Books" },
                    new Theme { Id = 2, ThemeName = "Elephants" });

            modelBuilder.Entity<ForumThread>().HasData(
                new ForumThread { Id = 1, AuthorId = 1, Content = "Elephants are the largest existing land animals. " +
                "Three living species are currently recognised: the African bush elephant, the African forest elephant, and the Asian elephant." +
                " They are an informal grouping within the subfamily Elephantinae of the order Proboscidea; extinct members include the mastodons.",
                    Title = "Super elephants", ThemeId = 1, TimeCreated = DateTime.Now },
                new ForumThread  {Id = 2, AuthorId = 2, Content = "Let`s talk about Mykola Khvylovy and his novel 'I(Romance)' ", Title = "Mykola Khvylovy", ThemeId = 2, TimeCreated = DateTime.Now });

            modelBuilder.Entity<Post>().HasData(
                new Post {Id = 1, ThreadId = 1, Content = "Man i love elephants!" +
                "I recently learned that elephants drink up to 300 liters of water a day!",
                    AuthorId = 2, TimeCreated = DateTime.Now },
                new Post {Id = 2, ThreadId = 1, Content = "My favourite elephant is Asian elephant", AuthorId = 3, TimeCreated = DateTime.Now },
                new Post {Id = 3, ThreadId = 2, Content = "Books are great you know.", AuthorId = 5, TimeCreated = DateTime.Now },
                new Post {Id = 4, ThreadId = 2, Content = "Read recently about Segriy Zhadan... He is cool.", AuthorId = 1, TimeCreated = DateTime.Now });
        }
    }
}

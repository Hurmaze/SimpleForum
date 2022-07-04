using DAL.DbAccess.Helper;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbAccess
{
    /// <summary>
    /// ForumDbContext
    /// </summary>
    public class ForumDbContext : DbContext
    {
        /// <summary>
        /// DbSet of Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// DbSet of Posts
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// DbSet of ForumThreads
        /// </summary>
        public DbSet<ForumThread> Threads { get; set; }

        /// <summary>
        /// DbSet of Themes
        /// </summary>
        public DbSet<Theme> Themes { get; set; }

        /// <summary>
        /// DbSet of Roles
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// DbSet of Accounts
        /// </summary>
        public DbSet<Credentials> Credentials { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForumDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Configures the database
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FluentApi
            var user = modelBuilder.Entity<User>();
            user.HasMany(u => u.ThreadPosts)
                .WithOne(t => t.Author)
                .OnDelete(DeleteBehavior.SetNull);
            user.HasMany(u => u.Threads)
                .WithOne(t => t.Author)
                .OnDelete(DeleteBehavior.SetNull);
            user.HasOne(u => u.Credentials)
                .WithOne(p => p.User)
                .HasForeignKey<Credentials>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            user.HasIndex(u => u.Email).IsUnique();
            user.Property(u => u.Email).HasMaxLength(100).IsRequired();
            user.Property(u => u.Nickname).HasMaxLength(30);
            user.Property(u => u.RegistrationTime).HasDefaultValueSql("GETDATE()");
            user.ToTable("Users");

            var thread = modelBuilder.Entity<ForumThread>();
            thread.HasMany(t => t.ThreadPosts)
                .WithOne(p => p.Thread)
                .OnDelete(DeleteBehavior.Cascade);
            thread.Property(t => t.Title).HasMaxLength(100).IsRequired();
            thread.Property(t => t.Content).IsRequired();
            thread.Property(t => t.TimeCreated).HasDefaultValueSql("GETDATE()");
            thread.ToTable("Threads");

            var theme = modelBuilder.Entity<Theme>();
            theme.HasMany(t => t.ForumThreads)
                .WithOne(ft => ft.Theme)
                .OnDelete(DeleteBehavior.SetNull);
            theme.Property(th => th.ThemeName).HasMaxLength(50).IsRequired();
            theme.ToTable("Themes");

            var post = modelBuilder.Entity<Post>();
            post.Property(p => p.TimeCreated).HasDefaultValueSql("GETDATE()");
            post.Property(p => p.Content).IsRequired();
            post.ToTable("Posts");

            var credentials = modelBuilder.Entity<Credentials>();
            credentials.HasOne(p => p.Role)
                .WithMany(r => r.Credentials)
                .OnDelete(DeleteBehavior.SetNull);
            credentials.Property(p => p.PasswordSalt).IsRequired();
            credentials.Property(p => p.PasswordHash).IsRequired();
            credentials.ToTable("Credentials");

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName).HasMaxLength(50).IsRequired();
            #endregion

            Seed(modelBuilder);
        }

        /// <summary>
        /// Data seeder
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Email = "user1@gmail.com", Nickname = "user1" },
                    new User { Id = 2, Email = "user2@gmail.com", Nickname = null },
                    new User { Id = 3, Email = "user3@gmail.com", Nickname = "user3" },
                    new User { Id = 4, Email = "moderator1@gmail.com", Nickname = "moderator1" },
                    new User { Id = 5, Email = "admin1@gmail.com", Nickname = "admin1" });

            modelBuilder.Entity<Theme>().HasData(
                    new Theme { Id = 1, ThemeName = "Books" },
                    new Theme { Id = 2, ThemeName = "Elephants" },
                    new Theme { Id = 3, ThemeName = "Other"});

            modelBuilder.Entity<ForumThread>().HasData(
                new ForumThread
                {
                    Id = 1,
                    AuthorId = 1,
                    Content = "Elephants are the largest existing land animals. " +
                "Three living species are currently recognised: the African bush elephant, the African forest elephant, and the Asian elephant." +
                " They are an informal grouping within the subfamily Elephantinae of the order Proboscidea; extinct members include the mastodons.",
                    Title = "Super elephants",
                    ThemeId = 1,
                    TimeCreated = DateTime.Now
                },
                new ForumThread { Id = 2, AuthorId = 2, Content = "Let`s talk about Mykola Khvylovy and his novel 'I(Romance)' ", Title = "Mykola Khvylovy", ThemeId = 2, TimeCreated = DateTime.Now });

            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    ThreadId = 1,
                    Content = "Man i love elephants!" +
                "I recently learned that elephants drink up to 300 liters of water a day!",
                    AuthorId = 2,
                    TimeCreated = DateTime.Now
                },
                new Post { Id = 2, ThreadId = 1, Content = "My favourite elephant is Asian elephant", AuthorId = 3, TimeCreated = DateTime.Now },
                new Post { Id = 3, ThreadId = 2, Content = "Books are great you know.", AuthorId = 5, TimeCreated = DateTime.Now },
                new Post { Id = 4, ThreadId = 2, Content = "Read recently about Segriy Zhadan... He is cool.", AuthorId = 1, TimeCreated = DateTime.Now });

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = Enum.GetName(BasicRoles.User).ToLower() },
                new Role { Id = 2, RoleName = Enum.GetName(BasicRoles.Moderator).ToLower() },
                new Role { Id = 3, RoleName = Enum.GetName(BasicRoles.Admin).ToLower() }
                );

            var accounts = new List<Credentials>
            {
                new Credentials { Id = 1, RoleId = 1, UserId = 1},
                new Credentials { Id = 2, RoleId = 1, UserId = 2},
                new Credentials { Id = 3, RoleId = 1, UserId = 3},
                new Credentials { Id = 4, RoleId = 2, UserId = 4},
                new Credentials { Id = 5, RoleId = 3, UserId = 5}
            };
            for (int i = 0; i < accounts.Count; i++)
            {
                accounts[i] = CredentialsCreation.CreateCredentials(accounts[i], "Passw0rd");
            }

            modelBuilder.Entity<Credentials>().HasData(
                accounts);
        }
    }
}

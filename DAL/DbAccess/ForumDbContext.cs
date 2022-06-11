using DAL.Entities.Forum;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbAccess
{
    public class ForumDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<ForumThread>? Threads { get; set; }
        public DbSet<Theme>? Themes { get; set; }

        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();
            user.HasMany(a => a.ThreadPosts).WithOne(t => t.Author);
            user.HasMany(a => a.Threads).WithOne(t => t.Author);
            user.HasIndex(a => a.Email).IsUnique();
            user.Property(a => a.Email).HasMaxLength(100).IsRequired();
            user.Property(a => a.Nickname).HasMaxLength(30);
            user.ToTable("Users");

            var thread = modelBuilder.Entity<ForumThread>();
            thread.ToTable("Threads");
            thread.HasOne(a => a.Theme).WithMany(t => t.ForumThreads);
            thread.HasMany(p => p.ThreadPosts).WithOne(c => c.Thread);
            thread.Property(p => p.Title).HasMaxLength(100);
            thread.Property(p => p.TimeCreated).HasDefaultValueSql("GETDATE()");

            var theme = modelBuilder.Entity<Theme>();
            theme.Property(th => th.ThemeName).HasMaxLength(50).IsRequired();

            var post = modelBuilder.Entity<Post>();
            post.Property(p => p.TimeCreated).HasDefaultValueSql("GETDATE()");
            post.ToTable("ThreadPosts");
        }
    }
}

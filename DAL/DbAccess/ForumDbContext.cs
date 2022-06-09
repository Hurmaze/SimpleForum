using DAL.Entities.Authentication;
using DAL.Entities.Forum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbAccess
{
    public class ForumDbContext : DbContext
    {
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<ForumThread>? Threads { get; set; }
        public DbSet<Theme>? Themes { get; set; }

        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var account = modelBuilder.Entity<Account>();
            account.HasMany(a => a.ThreadPosts).WithOne(t => t.Author);
            account.HasMany(a => a.Threads).WithOne(t => t.Author);
            account.HasIndex(a => a.Email).IsUnique();
            account.Property(a => a.Email).HasMaxLength(100);
            account.Property(a => a.Nickname).HasMaxLength(30);


            var thread = modelBuilder.Entity<ForumThread>();
            thread.ToTable("Threads");
            thread.HasOne(a => a.Theme).WithMany(t => t.ForumThreads);
            thread.HasMany(p => p.ThreadPosts).WithOne(c => c.Thread);
            thread.Property(p => p.Title).HasMaxLength(100);

            var theme = modelBuilder.Entity<Theme>();
            theme.Property(th => th.ThemeName).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Post>().ToTable("ThreadPosts");
        }
    }
}

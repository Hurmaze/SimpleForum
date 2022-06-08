using DAL.Entities;
using DAL.Entities.Enums;
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
        public DbSet<AccountAuth>? AccountAuths { get; set; }
        public DbSet<ThreadPost>? Comments { get; set; }
        public DbSet<ForumThread>? threads { get; set; }

        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var accountAuth = modelBuilder.Entity<AccountAuth>();
            accountAuth.HasOne(au => au.Account).WithOne(a => a.AccountAuth);
            accountAuth.HasIndex(a => a.Email).IsUnique();
            accountAuth.Property(a => a.PasswordSalt).IsRequired();
            accountAuth.Property(a => a.PasswordHash).IsRequired();

            var account = modelBuilder.Entity<Account>();
            account.HasMany(a => a.ThreadPosts).WithOne(t => t.Author);
            account.HasOne(a => a.AccountAuth).WithOne(au => au.Account);
            account.Property(a => a.Role).HasConversion(r =>
                r.ToString(),
                r => (Role)Enum.Parse(typeof(Role), r));

            var thread = modelBuilder.Entity<ForumThread>();
            thread.ToTable("Threads");
            thread.HasMany(p => p.ThreadPosts).WithOne(c => c.Thread);
            thread.Property(p => p.Title).HasMaxLength(100);
            thread.Property(p => p.Theme).HasConversion(th =>
                th.ToString(),
                th => (Theme)Enum.Parse(typeof(Theme), th)
            );

            modelBuilder.Entity<ThreadPost>().ToTable("ThreadPosts");
        }
    }
}

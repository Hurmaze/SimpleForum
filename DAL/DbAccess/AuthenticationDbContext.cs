using DAL.Entities.Authentication;
using Microsoft.EntityFrameworkCore;


namespace DAL.DbAccess
{
    public class AuthenticationDbContext : DbContext
    {
        public DbSet<Role>? Roles { get; set; }
        public DbSet<AccountAuth>? AccountAuths { get; set; }

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var accountAuth = modelBuilder.Entity<AccountAuth>();
            accountAuth.HasOne(a => a.Role).WithMany(r => r.AccountAuths);
            accountAuth.HasIndex(a => a.Email).IsUnique();
            accountAuth.Property(a => a.PasswordSalt).IsRequired();
            accountAuth.Property(a => a.PasswordHash).IsRequired();

            modelBuilder.Entity<Role>().Property(r => r.RoleName).HasMaxLength(50);
        }
    }
}

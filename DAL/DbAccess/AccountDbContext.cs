using DAL.Entities.Account;
using Microsoft.EntityFrameworkCore;


namespace DAL.DbAccess
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Role>? Roles { get; set; }
        public DbSet<Account>? Accounts { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var account = modelBuilder.Entity<Account>();
            account.HasOne(a => a.Role).WithMany(r => r.Accounts);
            account.HasIndex(a => a.Email).IsUnique();
            account.Property(a => a.PasswordSalt).IsRequired();
            account.Property(a => a.PasswordHash).IsRequired();
            account.Property(a => a.Email).IsRequired();
            account.ToTable("Accounts");

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName).HasMaxLength(50).IsRequired();
        }
    }
}

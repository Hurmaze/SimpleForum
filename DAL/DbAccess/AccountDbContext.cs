using DAL.DbAccess.Helper;
using DAL.Entities.Account;
using Microsoft.EntityFrameworkCore;


namespace DAL.DbAccess
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FluentApi
            var account = modelBuilder.Entity<Account>();
            account.HasOne(a => a.Role).WithMany(r => r.Accounts)
                .OnDelete(DeleteBehavior.NoAction);
            account.HasIndex(a => a.Email).IsUnique();
            account.Property(a => a.PasswordSalt).IsRequired();
            account.Property(a => a.PasswordHash).IsRequired();
            account.Property(a => a.Email).IsRequired();
            account.ToTable("Accounts");

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName).HasMaxLength(50).IsRequired();
            #endregion

            Seed(modelBuilder);
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = Enum.GetName(BasicRoles.User).ToLower() },
                new Role { Id = 2, RoleName = Enum.GetName(BasicRoles.Moderator).ToLower() },
                new Role { Id = 3, RoleName = Enum.GetName(BasicRoles.Admin).ToLower() }
                );

            var accounts = new List<Account>
            {
                new Account { Id = 1, RoleId = 1, Email = "user1@gmail.com"},
                new Account { Id = 2, RoleId = 1, Email = "user2@gmail.com"},
                new Account { Id = 3, RoleId = 1, Email = "user3@gmail.com"},
                new Account { Id = 4, RoleId = 2, Email = "moderator1@gmail.com"},
                new Account { Id = 5, RoleId = 3, Email = "admin1@gmail.com"}
            };
            for (int i = 0; i < accounts.Count; i++)
            {
                accounts[i] = AccountCreation.CreateAccount(accounts[i], "Passw0rd");
            }

            modelBuilder.Entity<Account>().HasData(
                accounts);
        }
    }
}

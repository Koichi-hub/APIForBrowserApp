using APIForBrowserApp.Database.Configurations;
using APIForBrowserApp.Entities;
using APIForBrowserApp.Models;
using APIForBrowserApp.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace APIForBrowserApp.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Student> Students { get; set; } = null!;

        public DbSet<Teacher> Teachers { get; set; } = null!;

        public DbSet<Group> Groups { get; set; } = null!;

        private readonly AppSettings appSettings;

        public DatabaseContext(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite(appSettings.ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new StudentConfiguration().Configure(modelBuilder.Entity<Student>());
            new TeacherConfiguration().Configure(modelBuilder.Entity<Teacher>());
            new GroupConfiguration().Configure(modelBuilder.Entity<Group>());

            var md5 = MD5.Create();
            var hashedPassword = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes("kek"))).Replace("-", "");
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Role = Enums.RolesEnum.Admin,
                Login = "admin",
                Password = hashedPassword,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified
                ));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

using APIForBrowserApp.Database.Configurations;
using APIForBrowserApp.Entities;
using APIForBrowserApp.Helpers;
using APIForBrowserApp.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Role = Enums.RolesEnum.Admin,
                Login = "admin",
                Password = UserPasswordHelper.HashPassword("kek"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
        }

        public override int SaveChanges()
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

            return base.SaveChanges();
        }
    }
}

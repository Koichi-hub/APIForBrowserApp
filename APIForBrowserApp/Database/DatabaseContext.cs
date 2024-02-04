using APIForBrowserApp.Database.Configurations;
using APIForBrowserApp.Entities;
using APIForBrowserApp.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace APIForBrowserApp.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users = null!;

        public DbSet<Student> Students = null!;

        public DbSet<Teacher> Teachers = null!;

        public DbSet<Group> Groups = null!;

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

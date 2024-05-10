using APIForBrowserApp.Entities;
using APIForBrowserApp.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIForBrowserApp.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .HasIndex(x => x.Login)
                .IsUnique();
            builder
                .Property(x => x.Role)
                .HasConversion(
                    v => (int)v,
                    v => (RolesEnum)v
                );
        }
    }
}

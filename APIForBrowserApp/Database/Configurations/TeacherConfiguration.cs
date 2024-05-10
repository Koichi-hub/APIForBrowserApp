using APIForBrowserApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIForBrowserApp.Database.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<Teacher>(x => x.UserId);
        }
    }
}

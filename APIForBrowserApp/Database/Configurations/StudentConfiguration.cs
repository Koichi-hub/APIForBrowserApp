using APIForBrowserApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIForBrowserApp.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<Student>(x => x.UserId);
        }
    }
}

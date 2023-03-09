using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRAssignment.Entity;

namespace SignalRAssignment.Configuration
{
    public class AppUsersConfiguration : IEntityTypeConfiguration<AppUsers>
    {
        public void Configure(EntityTypeBuilder<AppUsers> builder)
        {
            builder.ToTable("AppUsers");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(250);
        }
    }
}

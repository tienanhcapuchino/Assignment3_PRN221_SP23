using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRAssignment.Entity;

namespace SignalRAssignment.Configuration
{
    public class PostsCategoriesConfiguration : IEntityTypeConfiguration<PostCategories>
    {
        public void Configure(EntityTypeBuilder<PostCategories> builder)
        {
            builder.ToTable("PostCategories");
            builder.HasKey(x => x.CategoryId);
            builder.Property(x => x.CategoryName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Description).HasMaxLength(250);
        }
    }
}

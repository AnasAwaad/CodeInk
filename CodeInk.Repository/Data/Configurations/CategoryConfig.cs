using CodeInk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeInk.Repository.Data.Configurations;
public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Configure properties
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        // Configure relationships
        builder.HasMany(c => c.BookCategories)
               .WithOne(bc => bc.Category)
               .HasForeignKey(bc => bc.CategoryId);
    }
}

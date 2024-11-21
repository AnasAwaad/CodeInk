using CodeInk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeInk.Repository.Data.Configurations;
public class BookCategoryConfig : IEntityTypeConfiguration<BookCategory>
{
    public void Configure(EntityTypeBuilder<BookCategory> builder)
    {
        // Configure properties
        builder.HasKey(bc => new { bc.BookId, bc.CategoryId });

        // Configure relationships
        builder.HasOne(bc => bc.Book)
               .WithMany(b => b.BookCategories)
               .HasForeignKey(bc => bc.BookId);

        builder.HasOne(bc => bc.Category)
               .WithMany(c => c.BookCategories)
               .HasForeignKey(bc => bc.CategoryId);
    }
}

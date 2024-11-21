using CodeInk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeInk.Repository.Data.Configurations;
public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        // Configure properties
        builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Author).IsRequired().HasMaxLength(100);
        builder.Property(b => b.Description).HasMaxLength(1000);
        builder.Property(b => b.Price).HasColumnType("decimal(18,2)");
        builder.Property(b => b.CoverImageUrl).HasMaxLength(500);

        // Make ISBN unique
        builder.HasIndex(b => b.ISBN).IsUnique();

        // Configure relationships
        builder.HasMany(b => b.BookCategories)
               .WithOne(bc => bc.Book)
               .HasForeignKey(bc => bc.BookId);
    }
}

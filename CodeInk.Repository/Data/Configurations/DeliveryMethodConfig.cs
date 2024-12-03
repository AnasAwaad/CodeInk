using CodeInk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeInk.Repository.Data.Configurations;
public class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        // Configure properties
        builder.Property(c => c.ShortName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Description).IsRequired().HasMaxLength(500);
        builder.Property(c => c.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(c => c.DeliveryTime).IsRequired().HasMaxLength(100);


    }
}

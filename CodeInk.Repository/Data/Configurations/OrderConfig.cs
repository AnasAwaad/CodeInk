using CodeInk.Core.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeInk.Repository.Data.Configurations;
public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Converts the OrderStatus enum to its string representation when storing in the database
        // and back to the OrderStatus enum when reading from the database
        builder.Property(e => e.Status)
            .HasConversion(status => status.ToString(),
                           status => (OrderStatus)Enum.Parse(typeof(OrderStatus), status));

        builder.Property(e => e.SubTotal)
            .HasColumnType("decimal(18,2)");

        // Configures the ShippingAddress as a value object owned by the Order entity
        // This enables embedding ShippingAddress properties ( in the same table as the Order entity )
        builder.OwnsOne(e => e.ShippingAddress, shippingAddress => shippingAddress.WithOwner());

        builder.HasOne(e => e.DeliveryMethod)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}

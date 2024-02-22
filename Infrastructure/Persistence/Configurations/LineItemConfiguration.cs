using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
internal sealed class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.HasKey(li => li.Id);

        // conversion of strongly type id to primitive type in the database
        builder.Property(li => li.Id).HasConversion(
            lineItemId => lineItemId.Value, // map to lineItemId to the value
            value => new LineItemId(value)); // map back the value to a new lineItemId

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(li => li.ProductId);

        builder.OwnsOne(li => li.Price, priceBuilder =>
        {
            priceBuilder.Property(m => m.Currency)
                .HasMaxLength(3);

            priceBuilder.Property(m => m.Amount)
                .HasColumnType("decimal(18,4)");
        });
    }
}

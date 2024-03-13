using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        // conversion of strongly type id to primitive type in the database
        builder.Property(p => p.Id).HasConversion(
            productId => productId.Value, // map to productId to the value
            value => new ProductId(value)); // map back the value to a new productId 

        // either own entity or value conversion, in this case we define sku as value conversion
        builder.Property(p => p.Sku).HasConversion(
            sku => sku.Value, // map sku prop to sku value which is string
            value => Sku.Create(value)!); // map back the value from db as sku object, with null forgiving operator

        // seeding entity with owned value object properties
        var id = new ProductId(Guid.Parse("6769EC6C-9926-4125-8873-67D670F7083C"));

        // Money complex value object. in this case we defined this as own entity
        builder.OwnsOne(p => p.Price, priceBuilder =>
        {
            priceBuilder.Property(m => m.Amount)
                .HasMaxLength(3);

            priceBuilder.Property(m => m.Amount)
                .HasColumnType("decimal(18,4)");
        });

        // The HasConversion has the value stored as a delimited string in the db,
        // splits it to the List when you are using it and then joins it back again when saving
        builder.Property(p => p.Categories)
            .HasConversion(
                from => string.Join("|", from),
                to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList(),
                new ValueComparer<List<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
            )
        );

        builder.Property(p => p.Sizes)
            .HasConversion(
                from => string.Join("|", from),
                to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList(),
                new ValueComparer<List<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
            )
        );

        builder.Property(p => p.SideDishes)
           .HasConversion(
               from => string.Join("|", from),
               to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList(),
               new ValueComparer<List<string>>(
                   (c1, c2) => c1.SequenceEqual(c2),
                   c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                   c => c.ToList()
           )
       );
    }
}

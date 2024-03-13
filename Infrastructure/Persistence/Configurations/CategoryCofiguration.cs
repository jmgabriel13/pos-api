using Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
internal sealed class CategoryCofiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        // conversion of strongly type id to primitive type in the database
        builder.Property(c => c.Id).HasConversion(
            categoryId => categoryId.Value,  // map to categoryId to the value
            value => new CategoryId(value)); // map back the value to a new categoryId

        builder.Property(c => c.Name).HasMaxLength(100);
    }
}

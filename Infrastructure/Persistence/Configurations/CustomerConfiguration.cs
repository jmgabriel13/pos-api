using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        // conversion of strongly type id to primitive type in the database
        builder.Property(c => c.Id).HasConversion(
            customerId => customerId.Value, // map to customerId to the value
            value => new CustomerId(value)); // map back the value to a new customerId

        builder.Property(c => c.Name).HasMaxLength(100);
    }
}

using Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(c => c.Id);

        // conversion of strongly type id to primitive type in the database
        builder.Property(c => c.Id).HasConversion(
            memberId => memberId.Value, // map to memberId to the value
            value => new MemberId(value)); // map back the value to a new memberId

        builder.Property(c => c.Email).HasMaxLength(255);

        builder.HasIndex(c => c.Email).IsUnique();
    }
}

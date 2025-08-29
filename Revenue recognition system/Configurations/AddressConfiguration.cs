using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_recognition_system.Models;

namespace Revenue_recognition_system.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Address");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Number)
            .IsRequired()
            .HasMaxLength(10);
        
        builder.Property(a => a.PostalCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasMany(a => a.Clients)
            .WithOne(c => c.Address)
            .HasForeignKey(c => c.AddressId);
    }
}
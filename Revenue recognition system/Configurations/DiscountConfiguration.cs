using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_recognition_system.Models;

namespace Revenue_recognition_system.Configurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.Percentage)
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Property(d => d.ActiveFrom)
            .IsRequired();

        builder.Property(d => d.ActiveTo)
            .IsRequired();

        builder.Property(d => d.IsGlobal)
            .IsRequired();

        builder.HasMany(d => d.ClientDiscounts)
            .WithOne(cd => cd.Discount)
            .HasForeignKey(cd => cd.DiscountId);

        builder.HasMany(d => d.SoftwareVersionDiscounts)
            .WithOne(svd => svd.Discount)
            .HasForeignKey(svd => svd.DiscountId);
    }
}

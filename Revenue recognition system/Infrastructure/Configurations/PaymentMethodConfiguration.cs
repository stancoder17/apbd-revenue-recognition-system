using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_recognition_system.Domain.Entities;

namespace Revenue_recognition_system.Configurations;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.HasKey(pm => pm.Id);

        builder.Property(pm => pm.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(pm => pm.Payments)
            .WithOne(p => p.PaymentMethod)
            .HasForeignKey(p => p.PaymentMethodId);
    }
}

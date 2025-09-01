using Revenue_recognition_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Revenue_recognition_system.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Date)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(1000)
            .IsRequired();
        
        builder.Property(p => p.StatusId)
            .HasColumnName("Status_Id");
        
        builder.Property(p => p.PaymentMethodId)
            .HasColumnName("PaymentMethod_Id");

        builder.Property(p => p.PaymentTargetId)
            .HasColumnName("PaymentTarget_Id");

        builder.HasOne(p => p.Status)
            .WithMany(s => s.Payments)
            .HasForeignKey(p => p.StatusId);
        
        builder.HasOne(p => p.PaymentMethod)
            .WithMany(pm => pm.Payments)
            .HasForeignKey(p => p.PaymentMethodId);

        builder.HasOne(p => p.PaymentTarget)
            .WithMany(pt => pt.Payments)
            .HasForeignKey(p => p.PaymentTargetId);
    }
}

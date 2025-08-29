using Revenue_recognition_system.Models;

namespace Revenue_recognition_system.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SoftwareConfiguration : IEntityTypeConfiguration<Software>
{
    public void Configure(EntityTypeBuilder<Software> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(s => s.Description)
               .HasMaxLength(300)
               .IsRequired();

        builder.Property(s => s.LicensePrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.HasOne(s => s.Category)
               .WithMany(c => c.Softwares)
               .HasForeignKey(s => s.CategoryId);

        builder.HasMany(s => s.Versions)
               .WithOne(v => v.Software)
               .HasForeignKey(v => v.SoftwareId);
    }
}

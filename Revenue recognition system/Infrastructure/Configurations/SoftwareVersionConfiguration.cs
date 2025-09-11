using Revenue_recognition_system.Domain.Entities;

namespace Revenue_recognition_system.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SoftwareVersionConfiguration : IEntityTypeConfiguration<SoftwareVersion>
{
    public void Configure(EntityTypeBuilder<SoftwareVersion> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.SoftwareId)
            .HasColumnName("Software_Id");

        builder.Property(v => v.VersionNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(v => v.ReleaseDate)
            .IsRequired();

        builder.HasMany(v => v.SoftwareVersionDiscounts)
            .WithOne(svd => svd.SoftwareVersion)
            .HasForeignKey(svd => svd.SoftwareVersionId);
    }
}

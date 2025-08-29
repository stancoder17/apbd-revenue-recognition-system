using Revenue_recognition_system.Models;

namespace Revenue_recognition_system.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(s => s.Payments)
            .WithOne(p => p.Status)
            .HasForeignKey("Status_Id");
    }
}

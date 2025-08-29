using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_recognition_system.Models;

namespace Revenue_recognition_system.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.ClientId)
            .HasColumnName("Client_Id");

        builder.Property(c => c.SoftwareVersionId)
            .HasColumnName("SoftwareVersion_Id");

        builder.Property(c => c.StartDate)
            .IsRequired();
 
        builder.Property(c => c.EndDate)
            .IsRequired();

        builder.Property(c => c.FinalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(c => c.SignedAt)
            .IsRequired(false);

        builder.HasOne(c => c.Client)
            .WithMany(c => c.Contracts)
            .HasForeignKey(c => c.ClientId);

        builder.HasMany(c => c.SupportExtensions)
            .WithOne(se => se.Contract)
            .HasForeignKey(se => se.ContractId);
    }
}
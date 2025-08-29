using Revenue_recognition_system.Models;

namespace Revenue_recognition_system.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SupportExtensionConfiguration : IEntityTypeConfiguration<SupportExtension>
{
    public void Configure(EntityTypeBuilder<SupportExtension> builder)
    {
        builder.HasKey(se => se.Id);

        builder.Property(se => se.ContractId)
            .HasColumnName("Contract_Id");

        builder.Property(se => se.NumberOfYears)
            .IsRequired();

        builder.Property(se => se.Price)
            .HasColumnType("decimal(7,2)")
            .IsRequired();
        
        builder.Property(se => se.PurchaseDate)
            .IsRequired();


        builder.HasOne(se => se.Contract)
            .WithMany(c => c.SupportExtensions)
            .HasForeignKey(se => se.ContractId);
    }
}

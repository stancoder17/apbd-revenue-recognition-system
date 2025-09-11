using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_recognition_system.Domain.Entities;

namespace Revenue_recognition_system.Configurations;

public class IndividualClientConfiguration : IEntityTypeConfiguration<IndividualClient>
{
    public void Configure(EntityTypeBuilder<IndividualClient> builder)
    {
        builder.HasBaseType<Client>();
        
        builder.Property(ic => ic.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ic => ic.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ic => ic.Pesel)
            .IsRequired()
            .HasMaxLength(11)
            .IsFixedLength()
            .HasColumnName("PESEL");

        builder.Property(ic => ic.IsDeleted)
            .IsRequired();
    }
}
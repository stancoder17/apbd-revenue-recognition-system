using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_recognition_system.Domain.Entities;

namespace Revenue_recognition_system.Infrastructure.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);
        
        builder.Property(c => c.AddressId)
            .IsRequired()
            .HasColumnName("Address_Id");

        builder.HasOne(c => c.Address)
            .WithMany(a => a.Clients)
            .HasForeignKey(c => c.AddressId);

        // TPH discriminator
        builder.HasDiscriminator<string>("ClientType")
            .HasValue<IndividualClient>("Individual")
            .HasValue<CompanyClient>("Company");
    }
}
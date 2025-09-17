using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_recognition_system.Domain.Constants;
using Revenue_recognition_system.Domain.Entities;

namespace Revenue_recognition_system.Infrastructure.Configurations;

public class CompanyClientConfiguration : IEntityTypeConfiguration<CompanyClient>
{
    public void Configure(EntityTypeBuilder<CompanyClient> builder)
    {
        builder.Property(cc => cc.Name)
            .IsRequired()
            .HasMaxLength(ClientConstraints.CompanyNameMaxLength);

        builder.Property(cc => cc.Krs)
            .IsRequired()
            .HasColumnName("KRS")
            .HasMaxLength(ClientConstraints.KrsLength)
            .IsFixedLength();
        
        builder.HasIndex(cc => cc.Krs).IsUnique();
    }
}
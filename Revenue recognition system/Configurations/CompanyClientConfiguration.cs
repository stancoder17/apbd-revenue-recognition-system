using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_recognition_system.Models;

namespace Revenue_recognition_system.Configurations;

public class CompanyClientConfiguration : IEntityTypeConfiguration<CompanyClient>
{
    public void Configure(EntityTypeBuilder<CompanyClient> builder)
    {
        builder.Property(cc => cc.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cc => cc.Krs)
            .IsRequired()
            .HasColumnName("KRS")
            .HasMaxLength(10)
            .IsFixedLength();
    }
}
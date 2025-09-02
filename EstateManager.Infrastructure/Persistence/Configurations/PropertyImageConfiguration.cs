using EstateManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstateManager.Infrastructure.Persistence.Configurations;

public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
{
    public void Configure(EntityTypeBuilder<PropertyImage> builder)
    {
        builder.HasKey(i => i.IdPropertyImage);

        builder.Property(i => i.IdPropertyImage)
               .ValueGeneratedOnAdd(); 

        builder.Property(i => i.File)
               .IsRequired()
               .HasMaxLength(500);

        builder.HasOne(i => i.Property)
               .WithMany(p => p.Images)
               .HasForeignKey(i => i.IdProperty)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

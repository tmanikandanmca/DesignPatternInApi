using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductPhotoConfig : IEntityTypeConfiguration<ProductPhoto>
{
    public void Configure(EntityTypeBuilder<ProductPhoto> entity)
    {
        entity.HasKey(e => e.ProductPhotoID).HasName("PK_ProductPhoto_ProductPhotoID");

        entity.ToTable("ProductPhoto", "Production", tb => tb.HasComment("Product images."));

        entity.Property(e => e.ProductPhotoID).HasComment("Primary key for ProductPhoto records.");
        entity.Property(e => e.LargePhoto).HasComment("Large image of the product.");
        entity.Property(e => e.LargePhotoFileName)
            .HasMaxLength(50)
            .HasComment("Large image file name.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.ThumbNailPhoto).HasComment("Small image of the product.");
        entity.Property(e => e.ThumbnailPhotoFileName)
            .HasMaxLength(50)
            .HasComment("Small image file name.");

    }
}

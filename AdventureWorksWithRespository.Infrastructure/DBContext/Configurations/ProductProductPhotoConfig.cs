using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductProductPhotoConfig : IEntityTypeConfiguration<ProductProductPhoto>
{
    public void Configure(EntityTypeBuilder<ProductProductPhoto> entity)
    {
        entity.HasKey(e => new { e.ProductID, e.ProductPhotoID })
        .HasName("PK_ProductProductPhoto_ProductID_ProductPhotoID")
        .IsClustered(false);

        entity.ToTable("ProductProductPhoto", "Production", tb => tb.HasComment("Cross-reference table mapping products and product photos."));

        entity.Property(e => e.ProductID).HasComment("Product identification number. Foreign key to Product.ProductID.");
        entity.Property(e => e.ProductPhotoID).HasComment("Product photo identification number. Foreign key to ProductPhoto.ProductPhotoID.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Primary).HasComment("0 = Photo is not the principal image. 1 = Photo is the principal image.");

        entity.HasOne(d => d.Product).WithMany(p => p.ProductProductPhotos)
            .HasForeignKey(d => d.ProductID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.ProductPhoto).WithMany(p => p.ProductProductPhotos)
            .HasForeignKey(d => d.ProductPhotoID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

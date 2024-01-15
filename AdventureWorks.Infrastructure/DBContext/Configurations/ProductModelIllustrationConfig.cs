using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductModelIllustrationConfig : IEntityTypeConfiguration<ProductModelIllustration>
{
    public void Configure(EntityTypeBuilder<ProductModelIllustration> entity)
    {
        entity
            .HasKey(e => new { e.ProductModelID, e.IllustrationID })
            .HasName("PK_ProductModelIllustration_ProductModelID_IllustrationID");

        entity
            .ToTable("ProductModelIllustration", "Production", 
            tb => tb.HasComment("Cross-reference table mapping product models and illustrations."));

        entity
            .Property(e => e.ProductModelID)
            .HasComment("Primary key. Foreign key to ProductModel.ProductModelID.");

        entity
            .Property(e => e.IllustrationID)
            .HasComment("Primary key. Foreign key to Illustration.IllustrationID.");

        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");

        entity
            .HasOne(d => d.Illustration)
            .WithMany(p => p.ProductModelIllustrations)
            .HasForeignKey(d => d.IllustrationID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity
            .HasOne(d => d.ProductModel)
            .WithMany(p => p.ProductModelIllustrations)
            .HasForeignKey(d => d.ProductModelID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductDescriptionConfig : IEntityTypeConfiguration<ProductDescription>
{
    public void Configure(EntityTypeBuilder<ProductDescription> entity)
    {
        entity.HasKey(e => e.ProductDescriptionID).HasName("PK_ProductDescription_ProductDescriptionID");

        entity.ToTable("ProductDescription", "Production", tb => tb.HasComment("Product descriptions in several languages."));

        entity.HasIndex(e => e.rowguid, "AK_ProductDescription_rowguid").IsUnique();

        entity.Property(e => e.ProductDescriptionID).HasComment("Primary key for ProductDescription records.");
        entity.Property(e => e.Description)
            .HasMaxLength(400)
            .HasComment("Description of the product.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

    }
}

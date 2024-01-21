using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductModelConfig : IEntityTypeConfiguration<ProductModel>
{
    public void Configure(EntityTypeBuilder<ProductModel> entity)
    {
        entity.HasKey(e => e.ProductModelID).HasName("PK_ProductModel_ProductModelID");

        entity.ToTable("ProductModel", "Production", tb => tb.HasComment("Product model classification."));

        entity.HasIndex(e => e.Name, "AK_ProductModel_Name").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_ProductModel_rowguid").IsUnique();

        entity.HasIndex(e => e.CatalogDescription, "PXML_ProductModel_CatalogDescription");

        entity.HasIndex(e => e.Instructions, "PXML_ProductModel_Instructions");

        entity.Property(e => e.ProductModelID).HasComment("Primary key for ProductModel records.");
        entity.Property(e => e.CatalogDescription)
            .HasComment("Detailed product catalog information in xml format.")
            .HasColumnType("xml");
        entity.Property(e => e.Instructions)
            .HasComment("Manufacturing instructions in xml format.")
            .HasColumnType("xml");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Product model description.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

    }
}

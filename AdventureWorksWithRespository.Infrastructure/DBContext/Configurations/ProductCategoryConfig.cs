using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> entity)
    {
        entity.HasKey(e => e.ProductCategoryID).HasName("PK_ProductCategory_ProductCategoryID");

        entity.ToTable("ProductCategory", "Production", tb => tb.HasComment("High-level product categorization."));

        entity.HasIndex(e => e.Name, "AK_ProductCategory_Name").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_ProductCategory_rowguid").IsUnique();

        entity.Property(e => e.ProductCategoryID).HasComment("Primary key for ProductCategory records.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Category description.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

    }
}

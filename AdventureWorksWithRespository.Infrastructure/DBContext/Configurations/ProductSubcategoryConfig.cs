using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductSubcategoryConfig : IEntityTypeConfiguration<ProductSubcategory>
{
    public void Configure(EntityTypeBuilder<ProductSubcategory> entity)
    {
        entity.HasKey(e => e.ProductSubcategoryID).HasName("PK_ProductSubcategory_ProductSubcategoryID");

        entity.ToTable("ProductSubcategory", "Production", tb => tb.HasComment("Product subcategories. See ProductCategory table."));

        entity.HasIndex(e => e.Name, "AK_ProductSubcategory_Name").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_ProductSubcategory_rowguid").IsUnique();

        entity.Property(e => e.ProductSubcategoryID).HasComment("Primary key for ProductSubcategory records.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Subcategory description.");
        entity.Property(e => e.ProductCategoryID).HasComment("Product category identification number. Foreign key to ProductCategory.ProductCategoryID.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.ProductCategory).WithMany(p => p.ProductSubcategories)
            .HasForeignKey(d => d.ProductCategoryID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

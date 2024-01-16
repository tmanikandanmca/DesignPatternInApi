using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductVendorConfig : IEntityTypeConfiguration<ProductVendor>
{
    public void Configure(EntityTypeBuilder<ProductVendor> entity)
    {
        entity.HasKey(e => new { e.ProductID, e.BusinessEntityID }).HasName("PK_ProductVendor_ProductID_BusinessEntityID");

        entity.ToTable("ProductVendor", "Purchasing", tb => tb.HasComment("Cross-reference table mapping vendors with the products they supply."));

        entity.HasIndex(e => e.BusinessEntityID, "IX_ProductVendor_BusinessEntityID");

        entity.HasIndex(e => e.UnitMeasureCode, "IX_ProductVendor_UnitMeasureCode");

        entity.Property(e => e.ProductID).HasComment("Primary key. Foreign key to Product.ProductID.");
        entity.Property(e => e.BusinessEntityID).HasComment("Primary key. Foreign key to Vendor.BusinessEntityID.");
        entity.Property(e => e.AverageLeadTime).HasComment("The average span of time (in days) between placing an order with the vendor and receiving the purchased product.");
        entity.Property(e => e.LastReceiptCost)
            .HasComment("The selling price when last purchased.")
            .HasColumnType("money");
        entity.Property(e => e.LastReceiptDate)
            .HasComment("Date the product was last received by the vendor.")
            .HasColumnType("datetime");
        entity.Property(e => e.MaxOrderQty).HasComment("The minimum quantity that should be ordered.");
        entity.Property(e => e.MinOrderQty).HasComment("The maximum quantity that should be ordered.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.OnOrderQty).HasComment("The quantity currently on order.");
        entity.Property(e => e.StandardPrice)
            .HasComment("The vendor's usual selling price.")
            .HasColumnType("money");
        entity.Property(e => e.UnitMeasureCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasComment("The product's unit of measure.");

        entity.HasOne(d => d.BusinessEntity).WithMany(p => p.ProductVendors)
            .HasForeignKey(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Product).WithMany(p => p.ProductVendors)
            .HasForeignKey(d => d.ProductID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.UnitMeasureCodeNavigation).WithMany(p => p.ProductVendors)
            .HasForeignKey(d => d.UnitMeasureCode)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

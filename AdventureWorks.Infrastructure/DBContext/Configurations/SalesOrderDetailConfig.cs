using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class SalesOrderDetailConfig : IEntityTypeConfiguration<SalesOrderDetail>
{
    public void Configure(EntityTypeBuilder<SalesOrderDetail> entity)
    {
        entity.HasKey(e => new { e.SalesOrderID, e.SalesOrderDetailID }).HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

        entity.ToTable("SalesOrderDetail", "Sales", tb =>
        {
            tb.HasComment("Individual products associated with a specific sales order. See SalesOrderHeader.");
            tb.HasTrigger("iduSalesOrderDetail");
        });

        entity.HasIndex(e => e.rowguid, "AK_SalesOrderDetail_rowguid").IsUnique();

        entity.HasIndex(e => e.ProductID, "IX_SalesOrderDetail_ProductID");

        entity.Property(e => e.SalesOrderID).HasComment("Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");
        entity.Property(e => e.SalesOrderDetailID)
            .ValueGeneratedOnAdd()
            .HasComment("Primary key. One incremental unique number per product sold.");
        entity.Property(e => e.CarrierTrackingNumber)
            .HasMaxLength(25)
            .HasComment("Shipment tracking number supplied by the shipper.");
        entity.Property(e => e.LineTotal)
            .HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))", false)
            .HasComment("Per product subtotal. Computed as UnitPrice * (1 - UnitPriceDiscount) * OrderQty.")
            .HasColumnType("numeric(38, 6)");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.OrderQty).HasComment("Quantity ordered per product.");
        entity.Property(e => e.ProductID).HasComment("Product sold to customer. Foreign key to Product.ProductID.");
        entity.Property(e => e.SpecialOfferID).HasComment("Promotional code. Foreign key to SpecialOffer.SpecialOfferID.");
        entity.Property(e => e.UnitPrice)
            .HasComment("Selling price of a single product.")
            .HasColumnType("money");
        entity.Property(e => e.UnitPriceDiscount)
            .HasComment("Discount amount.")
            .HasColumnType("money");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.SalesOrder).WithMany(p => p.SalesOrderDetails).HasForeignKey(d => d.SalesOrderID);

        entity.HasOne(d => d.SpecialOfferProduct).WithMany(p => p.SalesOrderDetails)
            .HasForeignKey(d => new { d.SpecialOfferID, d.ProductID })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SalesOrderDetail_SpecialOfferProduct_SpecialOfferIDProductID");

    }
}

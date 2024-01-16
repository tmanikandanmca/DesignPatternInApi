using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class PurchaseOrderDetailConfig : IEntityTypeConfiguration<PurchaseOrderDetail>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderDetail> entity)
    {
        entity.HasKey(e => new { e.PurchaseOrderID, e.PurchaseOrderDetailID }).HasName("PK_PurchaseOrderDetail_PurchaseOrderID_PurchaseOrderDetailID");

        entity.ToTable("PurchaseOrderDetail", "Purchasing", tb =>
        {
            tb.HasComment("Individual products associated with a specific purchase order. See PurchaseOrderHeader.");
            tb.HasTrigger("iPurchaseOrderDetail");
            tb.HasTrigger("uPurchaseOrderDetail");
        });

        entity.HasIndex(e => e.ProductID, "IX_PurchaseOrderDetail_ProductID");

        entity.Property(e => e.PurchaseOrderID).HasComment("Primary key. Foreign key to PurchaseOrderHeader.PurchaseOrderID.");
        entity.Property(e => e.PurchaseOrderDetailID)
            .ValueGeneratedOnAdd()
            .HasComment("Primary key. One line number per purchased product.");
        entity.Property(e => e.DueDate)
            .HasComment("Date the product is expected to be received.")
            .HasColumnType("datetime");
        entity.Property(e => e.LineTotal)
            .HasComputedColumnSql("(isnull([OrderQty]*[UnitPrice],(0.00)))", false)
            .HasComment("Per product subtotal. Computed as OrderQty * UnitPrice.")
            .HasColumnType("money");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.OrderQty).HasComment("Quantity ordered.");
        entity.Property(e => e.ProductID).HasComment("Product identification number. Foreign key to Product.ProductID.");
        entity.Property(e => e.ReceivedQty)
            .HasComment("Quantity actually received from the vendor.")
            .HasColumnType("decimal(8, 2)");
        entity.Property(e => e.RejectedQty)
            .HasComment("Quantity rejected during inspection.")
            .HasColumnType("decimal(8, 2)");
        entity.Property(e => e.StockedQty)
            .HasComputedColumnSql("(isnull([ReceivedQty]-[RejectedQty],(0.00)))", false)
            .HasComment("Quantity accepted into inventory. Computed as ReceivedQty - RejectedQty.")
            .HasColumnType("decimal(9, 2)");
        entity.Property(e => e.UnitPrice)
            .HasComment("Vendor's selling price of a single product.")
            .HasColumnType("money");

        entity.HasOne(d => d.Product).WithMany(p => p.PurchaseOrderDetails)
            .HasForeignKey(d => d.ProductID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.PurchaseOrderDetails)
            .HasForeignKey(d => d.PurchaseOrderID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class PurchaseOrderHeaderConfig : IEntityTypeConfiguration<PurchaseOrderHeader>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderHeader> entity)
    {
        entity.HasKey(e => e.PurchaseOrderID).HasName("PK_PurchaseOrderHeader_PurchaseOrderID");

        entity.ToTable("PurchaseOrderHeader", "Purchasing", tb =>
        {
            tb.HasComment("General purchase order information. See PurchaseOrderDetail.");
            tb.HasTrigger("uPurchaseOrderHeader");
        });

        entity.HasIndex(e => e.EmployeeID, "IX_PurchaseOrderHeader_EmployeeID");

        entity.HasIndex(e => e.VendorID, "IX_PurchaseOrderHeader_VendorID");

        entity.Property(e => e.PurchaseOrderID).HasComment("Primary key.");
        entity.Property(e => e.EmployeeID).HasComment("Employee who created the purchase order. Foreign key to Employee.BusinessEntityID.");
        entity.Property(e => e.Freight)
            .HasComment("Shipping cost.")
            .HasColumnType("money");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.OrderDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Purchase order creation date.")
            .HasColumnType("datetime");
        entity.Property(e => e.RevisionNumber).HasComment("Incremental number to track changes to the purchase order over time.");
        entity.Property(e => e.ShipDate)
            .HasComment("Estimated shipment date from the vendor.")
            .HasColumnType("datetime");
        entity.Property(e => e.ShipMethodID).HasComment("Shipping method. Foreign key to ShipMethod.ShipMethodID.");
        entity.Property(e => e.Status)
            .HasDefaultValue((byte)1)
            .HasComment("Order current status. 1 = Pending; 2 = Approved; 3 = Rejected; 4 = Complete");
        entity.Property(e => e.SubTotal)
            .HasComment("Purchase order subtotal. Computed as SUM(PurchaseOrderDetail.LineTotal)for the appropriate PurchaseOrderID.")
            .HasColumnType("money");
        entity.Property(e => e.TaxAmt)
            .HasComment("Tax amount.")
            .HasColumnType("money");
        entity.Property(e => e.TotalDue)
            .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", true)
            .HasComment("Total due to vendor. Computed as Subtotal + TaxAmt + Freight.")
            .HasColumnType("money");
        entity.Property(e => e.VendorID).HasComment("Vendor with whom the purchase order is placed. Foreign key to Vendor.BusinessEntityID.");

        entity.HasOne(d => d.Employee).WithMany(p => p.PurchaseOrderHeaders)
            .HasForeignKey(d => d.EmployeeID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.ShipMethod).WithMany(p => p.PurchaseOrderHeaders)
            .HasForeignKey(d => d.ShipMethodID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrderHeaders)
            .HasForeignKey(d => d.VendorID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class WorkOrderConfig : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> entity)
    {
        entity.HasKey(e => e.WorkOrderID).HasName("PK_WorkOrder_WorkOrderID");

        entity.ToTable("WorkOrder", "Production", tb =>
        {
            tb.HasComment("Manufacturing work orders.");
            tb.HasTrigger("iWorkOrder");
            tb.HasTrigger("uWorkOrder");
        });

        entity.HasIndex(e => e.ProductID, "IX_WorkOrder_ProductID");

        entity.HasIndex(e => e.ScrapReasonID, "IX_WorkOrder_ScrapReasonID");

        entity.Property(e => e.WorkOrderID).HasComment("Primary key for WorkOrder records.");
        entity.Property(e => e.DueDate)
            .HasComment("Work order due date.")
            .HasColumnType("datetime");
        entity.Property(e => e.EndDate)
            .HasComment("Work order end date.")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.OrderQty).HasComment("Product quantity to build.");
        entity.Property(e => e.ProductID).HasComment("Product identification number. Foreign key to Product.ProductID.");
        entity.Property(e => e.ScrapReasonID).HasComment("Reason for inspection failure.");
        entity.Property(e => e.ScrappedQty).HasComment("Quantity that failed inspection.");
        entity.Property(e => e.StartDate)
            .HasComment("Work order start date.")
            .HasColumnType("datetime");
        entity.Property(e => e.StockedQty)
            .HasComputedColumnSql("(isnull([OrderQty]-[ScrappedQty],(0)))", false)
            .HasComment("Quantity built and put in inventory.");

        entity.HasOne(d => d.Product).WithMany(p => p.WorkOrders)
            .HasForeignKey(d => d.ProductID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.ScrapReason).WithMany(p => p.WorkOrders).HasForeignKey(d => d.ScrapReasonID);

    }
}

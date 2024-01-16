using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class WorkOrderRoutingConfig : IEntityTypeConfiguration<WorkOrderRouting>
{
    public void Configure(EntityTypeBuilder<WorkOrderRouting> entity)
    {
        entity.HasKey(e => new { e.WorkOrderID, e.ProductID, e.OperationSequence }).HasName("PK_WorkOrderRouting_WorkOrderID_ProductID_OperationSequence");

        entity.ToTable("WorkOrderRouting", "Production", tb => tb.HasComment("Work order details."));

        entity.HasIndex(e => e.ProductID, "IX_WorkOrderRouting_ProductID");

        entity.Property(e => e.WorkOrderID).HasComment("Primary key. Foreign key to WorkOrder.WorkOrderID.");
        entity.Property(e => e.ProductID).HasComment("Primary key. Foreign key to Product.ProductID.");
        entity.Property(e => e.OperationSequence).HasComment("Primary key. Indicates the manufacturing process sequence.");
        entity.Property(e => e.ActualCost)
            .HasComment("Actual manufacturing cost.")
            .HasColumnType("money");
        entity.Property(e => e.ActualEndDate)
            .HasComment("Actual end date.")
            .HasColumnType("datetime");
        entity.Property(e => e.ActualResourceHrs)
            .HasComment("Number of manufacturing hours used.")
            .HasColumnType("decimal(9, 4)");
        entity.Property(e => e.ActualStartDate)
            .HasComment("Actual start date.")
            .HasColumnType("datetime");
        entity.Property(e => e.LocationID).HasComment("Manufacturing location where the part is processed. Foreign key to Location.LocationID.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.PlannedCost)
            .HasComment("Estimated manufacturing cost.")
            .HasColumnType("money");
        entity.Property(e => e.ScheduledEndDate)
            .HasComment("Planned manufacturing end date.")
            .HasColumnType("datetime");
        entity.Property(e => e.ScheduledStartDate)
            .HasComment("Planned manufacturing start date.")
            .HasColumnType("datetime");

        entity.HasOne(d => d.Location).WithMany(p => p.WorkOrderRoutings)
            .HasForeignKey(d => d.LocationID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.WorkOrder).WithMany(p => p.WorkOrderRoutings)
            .HasForeignKey(d => d.WorkOrderID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

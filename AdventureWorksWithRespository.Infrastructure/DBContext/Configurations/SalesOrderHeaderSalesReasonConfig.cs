using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class SalesOrderHeaderSalesReasonConfig : IEntityTypeConfiguration<SalesOrderHeaderSalesReason>
{
    public void Configure(EntityTypeBuilder<SalesOrderHeaderSalesReason> entity)
    {
        entity.HasKey(e => new { e.SalesOrderID, e.SalesReasonID }).HasName("PK_SalesOrderHeaderSalesReason_SalesOrderID_SalesReasonID");

        entity.ToTable("SalesOrderHeaderSalesReason", "Sales", tb => tb.HasComment("Cross-reference table mapping sales orders to sales reason codes."));

        entity.Property(e => e.SalesOrderID).HasComment("Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");
        entity.Property(e => e.SalesReasonID).HasComment("Primary key. Foreign key to SalesReason.SalesReasonID.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");

        entity.HasOne(d => d.SalesOrder).WithMany(p => p.SalesOrderHeaderSalesReasons).HasForeignKey(d => d.SalesOrderID);

        entity.HasOne(d => d.SalesReason).WithMany(p => p.SalesOrderHeaderSalesReasons)
            .HasForeignKey(d => d.SalesReasonID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

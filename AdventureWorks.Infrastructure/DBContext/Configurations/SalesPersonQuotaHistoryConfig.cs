using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class SalesPersonQuotaHistoryConfig : IEntityTypeConfiguration<SalesPersonQuotaHistory>
{
    public void Configure(EntityTypeBuilder<SalesPersonQuotaHistory> entity)
    {
        entity.HasKey(e => new { e.BusinessEntityID, e.QuotaDate }).HasName("PK_SalesPersonQuotaHistory_BusinessEntityID_QuotaDate");

        entity.ToTable("SalesPersonQuotaHistory", "Sales", tb => tb.HasComment("Sales performance tracking."));

        entity.HasIndex(e => e.rowguid, "AK_SalesPersonQuotaHistory_rowguid").IsUnique();

        entity.Property(e => e.BusinessEntityID).HasComment("Sales person identification number. Foreign key to SalesPerson.BusinessEntityID.");
        entity.Property(e => e.QuotaDate)
            .HasComment("Sales quota date.")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.SalesQuota)
            .HasComment("Sales quota amount.")
            .HasColumnType("money");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.BusinessEntity).WithMany(p => p.SalesPersonQuotaHistories)
            .HasForeignKey(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

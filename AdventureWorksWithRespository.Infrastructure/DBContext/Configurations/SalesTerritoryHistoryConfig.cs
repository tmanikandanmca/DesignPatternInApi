using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class SalesTerritoryHistoryConfig : IEntityTypeConfiguration<SalesTerritoryHistory>
{
    public void Configure(EntityTypeBuilder<SalesTerritoryHistory> entity)
    {
        entity.HasKey(e => new { e.BusinessEntityID, e.StartDate, e.TerritoryID }).HasName("PK_SalesTerritoryHistory_BusinessEntityID_StartDate_TerritoryID");

        entity.ToTable("SalesTerritoryHistory", "Sales", tb => tb.HasComment("Sales representative transfers to other sales territories."));

        entity.HasIndex(e => e.rowguid, "AK_SalesTerritoryHistory_rowguid").IsUnique();

        entity.Property(e => e.BusinessEntityID).HasComment("Primary key. The sales rep.  Foreign key to SalesPerson.BusinessEntityID.");
        entity.Property(e => e.StartDate)
            .HasComment("Primary key. Date the sales representive started work in the territory.")
            .HasColumnType("datetime");
        entity.Property(e => e.TerritoryID).HasComment("Primary key. Territory identification number. Foreign key to SalesTerritory.SalesTerritoryID.");
        entity.Property(e => e.EndDate)
            .HasComment("Date the sales representative left work in the territory.")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.BusinessEntity).WithMany(p => p.SalesTerritoryHistories)
            .HasForeignKey(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Territory).WithMany(p => p.SalesTerritoryHistories)
            .HasForeignKey(d => d.TerritoryID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class SalesTerritoryConfig : IEntityTypeConfiguration<SalesTerritory>
{
    public void Configure(EntityTypeBuilder<SalesTerritory> entity)
    {
        entity.HasKey(e => e.TerritoryID).HasName("PK_SalesTerritory_TerritoryID");

        entity.ToTable("SalesTerritory", "Sales", tb => tb.HasComment("Sales territory lookup table."));

        entity.HasIndex(e => e.Name, "AK_SalesTerritory_Name").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_SalesTerritory_rowguid").IsUnique();

        entity.Property(e => e.TerritoryID).HasComment("Primary key for SalesTerritory records.");
        entity.Property(e => e.CostLastYear)
            .HasComment("Business costs in the territory the previous year.")
            .HasColumnType("money");
        entity.Property(e => e.CostYTD)
            .HasComment("Business costs in the territory year to date.")
            .HasColumnType("money");
        entity.Property(e => e.CountryRegionCode)
            .HasMaxLength(3)
            .HasComment("ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. ");
        entity.Property(e => e.Group)
            .HasMaxLength(50)
            .HasComment("Geographic area to which the sales territory belong.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Sales territory description");
        entity.Property(e => e.SalesLastYear)
            .HasComment("Sales in the territory the previous year.")
            .HasColumnType("money");
        entity.Property(e => e.SalesYTD)
            .HasComment("Sales in the territory year to date.")
            .HasColumnType("money");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.CountryRegionCodeNavigation).WithMany(p => p.SalesTerritories)
            .HasForeignKey(d => d.CountryRegionCode)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

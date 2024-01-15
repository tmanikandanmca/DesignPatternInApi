using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class StateProvinceConfig : IEntityTypeConfiguration<StateProvince>
{
    public void Configure(EntityTypeBuilder<StateProvince> entity)
    {
        entity.HasKey(e => e.StateProvinceID).HasName("PK_StateProvince_StateProvinceID");

        entity.ToTable("StateProvince", "Person", tb => tb.HasComment("State and province lookup table."));

        entity.HasIndex(e => e.Name, "AK_StateProvince_Name").IsUnique();

        entity.HasIndex(e => new { e.StateProvinceCode, e.CountryRegionCode }, "AK_StateProvince_StateProvinceCode_CountryRegionCode").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_StateProvince_rowguid").IsUnique();

        entity.Property(e => e.StateProvinceID).HasComment("Primary key for StateProvince records.");
        entity.Property(e => e.CountryRegionCode)
            .HasMaxLength(3)
            .HasComment("ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. ");
        entity.Property(e => e.IsOnlyStateProvinceFlag)
            .HasDefaultValue(true)
            .HasComment("0 = StateProvinceCode exists. 1 = StateProvinceCode unavailable, using CountryRegionCode.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("State or province description.");
        entity.Property(e => e.StateProvinceCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasComment("ISO standard state or province code.");
        entity.Property(e => e.TerritoryID).HasComment("ID of the territory in which the state or province is located. Foreign key to SalesTerritory.SalesTerritoryID.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.CountryRegionCodeNavigation).WithMany(p => p.StateProvinces)
            .HasForeignKey(d => d.CountryRegionCode)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Territory).WithMany(p => p.StateProvinces)
            .HasForeignKey(d => d.TerritoryID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

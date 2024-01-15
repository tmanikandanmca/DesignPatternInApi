using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class CountryRegionCurrencyConfig : IEntityTypeConfiguration<CountryRegionCurrency>
{
    public void Configure(EntityTypeBuilder<CountryRegionCurrency> entity)
    {
        entity.HasKey(e => new { e.CountryRegionCode, e.CurrencyCode }).HasName("PK_CountryRegionCurrency_CountryRegionCode_CurrencyCode");

        entity.ToTable("CountryRegionCurrency", "Sales", tb => tb.HasComment("Cross-reference table mapping ISO currency codes to a country or region."));

        entity.HasIndex(e => e.CurrencyCode, "IX_CountryRegionCurrency_CurrencyCode");

        entity.Property(e => e.CountryRegionCode)
            .HasMaxLength(3)
            .HasComment("ISO code for countries and regions. Foreign key to CountryRegion.CountryRegionCode.");
        entity.Property(e => e.CurrencyCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasComment("ISO standard currency code. Foreign key to Currency.CurrencyCode.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");

        entity.HasOne(d => d.CountryRegionCodeNavigation).WithMany(p => p.CountryRegionCurrencies)
            .HasForeignKey(d => d.CountryRegionCode)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.CurrencyCodeNavigation).WithMany(p => p.CountryRegionCurrencies)
            .HasForeignKey(d => d.CurrencyCode)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

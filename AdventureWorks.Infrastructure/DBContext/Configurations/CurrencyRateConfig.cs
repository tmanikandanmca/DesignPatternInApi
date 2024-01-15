using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class CurrencyRateConfig : IEntityTypeConfiguration<CurrencyRate>
{
    public void Configure(EntityTypeBuilder<CurrencyRate> entity)
    {
        entity.HasKey(e => e.CurrencyRateID).HasName("PK_CurrencyRate_CurrencyRateID");

        entity.ToTable("CurrencyRate", "Sales", tb => tb.HasComment("Currency exchange rates."));

        entity.HasIndex(e => new { e.CurrencyRateDate, e.FromCurrencyCode, e.ToCurrencyCode }, "AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode").IsUnique();

        entity.Property(e => e.CurrencyRateID).HasComment("Primary key for CurrencyRate records.");
        entity.Property(e => e.AverageRate)
            .HasComment("Average exchange rate for the day.")
            .HasColumnType("money");
        entity.Property(e => e.CurrencyRateDate)
            .HasComment("Date and time the exchange rate was obtained.")
            .HasColumnType("datetime");
        entity.Property(e => e.EndOfDayRate)
            .HasComment("Final exchange rate for the day.")
            .HasColumnType("money");
        entity.Property(e => e.FromCurrencyCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasComment("Exchange rate was converted from this currency code.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.ToCurrencyCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasComment("Exchange rate was converted to this currency code.");

        entity.HasOne(d => d.FromCurrencyCodeNavigation).WithMany(p => p.CurrencyRateFromCurrencyCodeNavigations)
            .HasForeignKey(d => d.FromCurrencyCode)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.ToCurrencyCodeNavigation).WithMany(p => p.CurrencyRateToCurrencyCodeNavigations)
            .HasForeignKey(d => d.ToCurrencyCode)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

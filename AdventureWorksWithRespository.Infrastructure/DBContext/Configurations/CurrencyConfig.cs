using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class CurrencyConfig : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> entity)
    {
        entity.HasKey(e => e.CurrencyCode).HasName("PK_Currency_CurrencyCode");

        entity.ToTable("Currency", "Sales", tb => tb.HasComment("Lookup table containing standard ISO currencies."));

        entity.HasIndex(e => e.Name, "AK_Currency_Name").IsUnique();

        entity.Property(e => e.CurrencyCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasComment("The ISO code for the Currency.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Currency name.");
    }
}

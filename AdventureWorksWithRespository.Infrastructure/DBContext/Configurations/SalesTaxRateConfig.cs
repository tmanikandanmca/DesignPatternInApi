using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class SalesTaxRateConfig : IEntityTypeConfiguration<SalesTaxRate>
{
    public void Configure(EntityTypeBuilder<SalesTaxRate> entity)
    {
        entity.HasKey(e => e.SalesTaxRateID).HasName("PK_SalesTaxRate_SalesTaxRateID");

        entity.ToTable("SalesTaxRate", "Sales", tb => tb.HasComment("Tax rate lookup table."));

        entity.HasIndex(e => new { e.StateProvinceID, e.TaxType }, "AK_SalesTaxRate_StateProvinceID_TaxType").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_SalesTaxRate_rowguid").IsUnique();

        entity.Property(e => e.SalesTaxRateID).HasComment("Primary key for SalesTaxRate records.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Tax rate description.");
        entity.Property(e => e.StateProvinceID).HasComment("State, province, or country/region the sales tax applies to.");
        entity.Property(e => e.TaxRate)
            .HasComment("Tax rate amount.")
            .HasColumnType("smallmoney");
        entity.Property(e => e.TaxType).HasComment("1 = Tax applied to retail transactions, 2 = Tax applied to wholesale transactions, 3 = Tax applied to all sales (retail and wholesale) transactions.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.StateProvince).WithMany(p => p.SalesTaxRates)
            .HasForeignKey(d => d.StateProvinceID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

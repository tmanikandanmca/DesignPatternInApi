using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class VendorConfig : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> entity)
    {
        entity.HasKey(e => e.BusinessEntityID).HasName("PK_Vendor_BusinessEntityID");

        entity.ToTable("Vendor", "Purchasing", tb =>
        {
            tb.HasComment("Companies from whom Adventure Works Cycles purchases parts or other goods.");
            tb.HasTrigger("dVendor");
        });

        entity.HasIndex(e => e.AccountNumber, "AK_Vendor_AccountNumber").IsUnique();

        entity.Property(e => e.BusinessEntityID)
            .ValueGeneratedNever()
            .HasComment("Primary key for Vendor records.  Foreign key to BusinessEntity.BusinessEntityID");
        entity.Property(e => e.AccountNumber)
            .HasMaxLength(15)
            .HasComment("Vendor account (identification) number.");
        entity.Property(e => e.ActiveFlag)
            .HasDefaultValue(true)
            .HasComment("0 = Vendor no longer used. 1 = Vendor is actively used.");
        entity.Property(e => e.CreditRating).HasComment("1 = Superior, 2 = Excellent, 3 = Above average, 4 = Average, 5 = Below average");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Company name.");
        entity.Property(e => e.PreferredVendorStatus)
            .HasDefaultValue(true)
            .HasComment("0 = Do not use if another vendor is available. 1 = Preferred over other vendors supplying the same product.");
        entity.Property(e => e.PurchasingWebServiceURL)
            .HasMaxLength(1024)
            .HasComment("Vendor URL.");

        entity.HasOne(d => d.BusinessEntity).WithOne(p => p.Vendor)
            .HasForeignKey<Vendor>(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

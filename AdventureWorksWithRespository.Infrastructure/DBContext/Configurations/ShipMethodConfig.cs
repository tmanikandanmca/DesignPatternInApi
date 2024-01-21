using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ShipMethodConfig : IEntityTypeConfiguration<ShipMethod>
{
    public void Configure(EntityTypeBuilder<ShipMethod> entity)
    {
        entity.HasKey(e => e.ShipMethodID).HasName("PK_ShipMethod_ShipMethodID");

        entity.ToTable("ShipMethod", "Purchasing", tb => tb.HasComment("Shipping company lookup table."));

        entity.HasIndex(e => e.Name, "AK_ShipMethod_Name").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_ShipMethod_rowguid").IsUnique();

        entity.Property(e => e.ShipMethodID).HasComment("Primary key for ShipMethod records.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Shipping company name.");
        entity.Property(e => e.ShipBase)
            .HasComment("Minimum shipping charge.")
            .HasColumnType("money");
        entity.Property(e => e.ShipRate)
            .HasComment("Shipping charge per pound.")
            .HasColumnType("money");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

    }
}

using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class AddressTypeConfig : IEntityTypeConfiguration<AddressType>
{
    public void Configure(EntityTypeBuilder<AddressType> entity)
    {
        entity.HasKey(e => e.AddressTypeID).HasName("PK_AddressType_AddressTypeID");

        entity.ToTable("AddressType", "Person", tb => tb.HasComment("Types of addresses stored in the Address table. "));

        entity.HasIndex(e => e.Name, "AK_AddressType_Name").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_AddressType_rowguid").IsUnique();

        entity.Property(e => e.AddressTypeID).HasComment("Primary key for AddressType records.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Address type description. For example, Billing, Home, or Shipping.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
    }
}

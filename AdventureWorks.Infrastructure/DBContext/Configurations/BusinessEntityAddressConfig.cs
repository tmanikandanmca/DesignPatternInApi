using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations
{
    internal class BusinessEntityAddressConfig : IEntityTypeConfiguration<BusinessEntityAddress>
    {
        public void Configure(EntityTypeBuilder<BusinessEntityAddress> entity)
        {
            entity.HasKey(e => new { e.BusinessEntityID, e.AddressID, e.AddressTypeID }).HasName("PK_BusinessEntityAddress_BusinessEntityID_AddressID_AddressTypeID");

            entity.ToTable("BusinessEntityAddress", "Person", tb => tb.HasComment("Cross-reference table mapping customers, vendors, and employees to their addresses."));

            entity.HasIndex(e => e.rowguid, "AK_BusinessEntityAddress_rowguid").IsUnique();

            entity.HasIndex(e => e.AddressID, "IX_BusinessEntityAddress_AddressID");

            entity.HasIndex(e => e.AddressTypeID, "IX_BusinessEntityAddress_AddressTypeID");

            entity.Property(e => e.BusinessEntityID).HasComment("Primary key. Foreign key to BusinessEntity.BusinessEntityID.");
            entity.Property(e => e.AddressID).HasComment("Primary key. Foreign key to Address.AddressID.");
            entity.Property(e => e.AddressTypeID).HasComment("Primary key. Foreign key to AddressType.AddressTypeID.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.Address).WithMany(p => p.BusinessEntityAddresses)
                .HasForeignKey(d => d.AddressID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.AddressType).WithMany(p => p.BusinessEntityAddresses)
                .HasForeignKey(d => d.AddressTypeID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.BusinessEntity).WithMany(p => p.BusinessEntityAddresses)
                .HasForeignKey(d => d.BusinessEntityID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}

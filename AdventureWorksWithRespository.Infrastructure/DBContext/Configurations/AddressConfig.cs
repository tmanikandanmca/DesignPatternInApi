using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class AddressConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> entity)
    {
        entity.HasKey(e => e.AddressID).HasName("PK_Address_AddressID");

        entity.ToTable("Address", "Person", tb => tb.HasComment("Street address information for customers, employees, and vendors."));

        entity.HasIndex(e => e.rowguid, "AK_Address_rowguid").IsUnique();

        entity.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvinceID, e.PostalCode }, "IX_Address_AddressLine1_AddressLine2_City_StateProvinceID_PostalCode").IsUnique();

        entity.HasIndex(e => e.StateProvinceID, "IX_Address_StateProvinceID");

        entity.Property(e => e.AddressID).HasComment("Primary key for Address records.");
        entity.Property(e => e.AddressLine1)
            .HasMaxLength(60)
            .HasComment("First street address line.");
        entity.Property(e => e.AddressLine2)
            .HasMaxLength(60)
            .HasComment("Second street address line.");
        entity.Property(e => e.City)
            .HasMaxLength(30)
            .HasComment("Name of the city.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.PostalCode)
            .HasMaxLength(15)
            .HasComment("Postal code for the street address.");
        entity.Property(e => e.StateProvinceID).HasComment("Unique identification number for the state or province. Foreign key to StateProvince table.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.StateProvince).WithMany(p => p.Addresses)
            .HasForeignKey(d => d.StateProvinceID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

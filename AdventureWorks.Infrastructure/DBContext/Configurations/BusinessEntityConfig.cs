using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class BusinessEntityConfig : IEntityTypeConfiguration<BusinessEntity>
{
    public void Configure(EntityTypeBuilder<BusinessEntity> entity)
    {
        entity.HasKey(e => e.BusinessEntityID).HasName("PK_BusinessEntity_BusinessEntityID");

        entity.ToTable("BusinessEntity", "Person", tb => tb.HasComment("Source of the ID that connects vendors, customers, and employees with address and contact information."));

        entity.HasIndex(e => e.rowguid, "AK_BusinessEntity_rowguid").IsUnique();

        entity.Property(e => e.BusinessEntityID).HasComment("Primary key for all customers, vendors, and employees.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

    }
}

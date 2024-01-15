using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.HasKey(e => e.CustomerID).HasName("PK_Customer_CustomerID");

        entity.ToTable("Customer", "Sales", tb => tb.HasComment("Current customer information. Also see the Person and Store tables."));

        entity.HasIndex(e => e.AccountNumber, "AK_Customer_AccountNumber").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_Customer_rowguid").IsUnique();

        entity.HasIndex(e => e.TerritoryID, "IX_Customer_TerritoryID");

        entity.Property(e => e.CustomerID).HasComment("Primary key.");
        entity.Property(e => e.AccountNumber)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasComputedColumnSql("(isnull('AW'+[dbo].[ufnLeadingZeros]([CustomerID]),''))", false)
            .HasComment("Unique number identifying the customer assigned by the accounting system.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.PersonID).HasComment("Foreign key to Person.BusinessEntityID");
        entity.Property(e => e.StoreID).HasComment("Foreign key to Store.BusinessEntityID");
        entity.Property(e => e.TerritoryID).HasComment("ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.Person).WithMany(p => p.Customers).HasForeignKey(d => d.PersonID);

        entity.HasOne(d => d.Store).WithMany(p => p.Customers).HasForeignKey(d => d.StoreID);

        entity.HasOne(d => d.Territory).WithMany(p => p.Customers).HasForeignKey(d => d.TerritoryID);
    }
}

using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class StoreConfig : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> entity)
    {
        entity.HasKey(e => e.BusinessEntityID).HasName("PK_Store_BusinessEntityID");

        entity.ToTable("Store", "Sales", tb => tb.HasComment("Customers (resellers) of Adventure Works products."));

        entity.HasIndex(e => e.rowguid, "AK_Store_rowguid").IsUnique();

        entity.HasIndex(e => e.SalesPersonID, "IX_Store_SalesPersonID");

        entity.HasIndex(e => e.Demographics, "PXML_Store_Demographics");

        entity.Property(e => e.BusinessEntityID)
            .ValueGeneratedNever()
            .HasComment("Primary key. Foreign key to Customer.BusinessEntityID.");
        entity.Property(e => e.Demographics)
            .HasComment("Demographic informationg about the store such as the number of employees, annual sales and store type.")
            .HasColumnType("xml");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Name of the store.");
        entity.Property(e => e.SalesPersonID).HasComment("ID of the sales person assigned to the customer. Foreign key to SalesPerson.BusinessEntityID.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.BusinessEntity).WithOne(p => p.Store)
            .HasForeignKey<Store>(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.SalesPerson).WithMany(p => p.Stores).HasForeignKey(d => d.SalesPersonID);

    }
}

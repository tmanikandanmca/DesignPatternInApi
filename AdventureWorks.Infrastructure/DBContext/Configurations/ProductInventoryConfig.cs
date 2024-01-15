using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductInventoryConfig : IEntityTypeConfiguration<ProductInventory>
{
    public void Configure(EntityTypeBuilder<ProductInventory> entity)
    {
        entity.HasKey(e => new { e.ProductID, e.LocationID }).HasName("PK_ProductInventory_ProductID_LocationID");

        entity.ToTable("ProductInventory", "Production", tb => tb.HasComment("Product inventory information."));

        entity.Property(e => e.ProductID).HasComment("Product identification number. Foreign key to Product.ProductID.");
        entity.Property(e => e.LocationID).HasComment("Inventory location identification number. Foreign key to Location.LocationID. ");
        entity.Property(e => e.Bin).HasComment("Storage container on a shelf in an inventory location.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Quantity).HasComment("Quantity of products in the inventory location.");
        entity.Property(e => e.Shelf)
            .HasMaxLength(10)
            .HasComment("Storage compartment within an inventory location.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.Location).WithMany(p => p.ProductInventories)
            .HasForeignKey(d => d.LocationID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Product).WithMany(p => p.ProductInventories)
            .HasForeignKey(d => d.ProductID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

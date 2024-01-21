using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.HasKey(e => e.ProductID).HasName("PK_Product_ProductID");

        entity.ToTable("Product", "Production", tb => tb.HasComment("Products sold or used in the manfacturing of sold products."));

        entity.HasIndex(e => e.Name, "AK_Product_Name").IsUnique();

        entity.HasIndex(e => e.ProductNumber, "AK_Product_ProductNumber").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_Product_rowguid").IsUnique();

        entity.Property(e => e.ProductID).HasComment("Primary key for Product records.");
        entity.Property(e => e.Class)
            .HasMaxLength(2)
            .IsFixedLength()
            .HasComment("H = High, M = Medium, L = Low");
        entity.Property(e => e.Color)
            .HasMaxLength(15)
            .HasComment("Product color.");
        entity.Property(e => e.DaysToManufacture).HasComment("Number of days required to manufacture the product.");
        entity.Property(e => e.DiscontinuedDate)
            .HasComment("Date the product was discontinued.")
            .HasColumnType("datetime");
        entity.Property(e => e.FinishedGoodsFlag)
            .HasDefaultValue(true)
            .HasComment("0 = Product is not a salable item. 1 = Product is salable.");
        entity.Property(e => e.ListPrice)
            .HasComment("Selling price.")
            .HasColumnType("money");
        entity.Property(e => e.MakeFlag)
            .HasDefaultValue(true)
            .HasComment("0 = Product is purchased, 1 = Product is manufactured in-house.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Name of the product.");
        entity.Property(e => e.ProductLine)
            .HasMaxLength(2)
            .IsFixedLength()
            .HasComment("R = Road, M = Mountain, T = Touring, S = Standard");
        entity.Property(e => e.ProductModelID).HasComment("Product is a member of this product model. Foreign key to ProductModel.ProductModelID.");
        entity.Property(e => e.ProductNumber)
            .HasMaxLength(25)
            .HasComment("Unique product identification number.");
        entity.Property(e => e.ProductSubcategoryID).HasComment("Product is a member of this product subcategory. Foreign key to ProductSubCategory.ProductSubCategoryID. ");
        entity.Property(e => e.ReorderPoint).HasComment("Inventory level that triggers a purchase order or work order. ");
        entity.Property(e => e.SafetyStockLevel).HasComment("Minimum inventory quantity. ");
        entity.Property(e => e.SellEndDate)
            .HasComment("Date the product was no longer available for sale.")
            .HasColumnType("datetime");
        entity.Property(e => e.SellStartDate)
            .HasComment("Date the product was available for sale.")
            .HasColumnType("datetime");
        entity.Property(e => e.Size)
            .HasMaxLength(5)
            .HasComment("Product size.");
        entity.Property(e => e.SizeUnitMeasureCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasComment("Unit of measure for Size column.");
        entity.Property(e => e.StandardCost)
            .HasComment("Standard cost of the product.")
            .HasColumnType("money");
        entity.Property(e => e.Style)
            .HasMaxLength(2)
            .IsFixedLength()
            .HasComment("W = Womens, M = Mens, U = Universal");
        entity.Property(e => e.Weight)
            .HasComment("Product weight.")
            .HasColumnType("decimal(8, 2)");
        entity.Property(e => e.WeightUnitMeasureCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasComment("Unit of measure for Weight column.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.ProductModel).WithMany(p => p.Products).HasForeignKey(d => d.ProductModelID);

        entity.HasOne(d => d.ProductSubcategory).WithMany(p => p.Products).HasForeignKey(d => d.ProductSubcategoryID);

        entity.HasOne(d => d.SizeUnitMeasureCodeNavigation).WithMany(p => p.ProductSizeUnitMeasureCodeNavigations).HasForeignKey(d => d.SizeUnitMeasureCode);

        entity.HasOne(d => d.WeightUnitMeasureCodeNavigation).WithMany(p => p.ProductWeightUnitMeasureCodeNavigations).HasForeignKey(d => d.WeightUnitMeasureCode);

    }
}

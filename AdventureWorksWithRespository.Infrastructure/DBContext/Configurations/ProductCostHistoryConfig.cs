using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductCostHistoryConfig : IEntityTypeConfiguration<ProductCostHistory>
{
    public void Configure(EntityTypeBuilder<ProductCostHistory> entity)
    {
        entity.HasKey(e => new { e.ProductID, e.StartDate }).HasName("PK_ProductCostHistory_ProductID_StartDate");

        entity.ToTable("ProductCostHistory", "Production", tb => tb.HasComment("Changes in the cost of a product over time."));

        entity.Property(e => e.ProductID).HasComment("Product identification number. Foreign key to Product.ProductID");
        entity.Property(e => e.StartDate)
            .HasComment("Product cost start date.")
            .HasColumnType("datetime");
        entity.Property(e => e.EndDate)
            .HasComment("Product cost end date.")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.StandardCost)
            .HasComment("Standard cost of the product.")
            .HasColumnType("money");

        entity.HasOne(d => d.Product).WithMany(p => p.ProductCostHistories)
            .HasForeignKey(d => d.ProductID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

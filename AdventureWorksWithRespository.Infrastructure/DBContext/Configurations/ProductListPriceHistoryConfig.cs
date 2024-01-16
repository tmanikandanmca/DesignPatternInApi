using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductListPriceHistoryConfig : IEntityTypeConfiguration<ProductListPriceHistory>
{
    public void Configure(EntityTypeBuilder<ProductListPriceHistory> entity)
    {
        entity.HasKey(e => new { e.ProductID, e.StartDate }).HasName("PK_ProductListPriceHistory_ProductID_StartDate");

        entity.ToTable("ProductListPriceHistory", "Production", tb => tb.HasComment("Changes in the list price of a product over time."));

        entity.Property(e => e.ProductID).HasComment("Product identification number. Foreign key to Product.ProductID");
        entity.Property(e => e.StartDate)
            .HasComment("List price start date.")
            .HasColumnType("datetime");
        entity.Property(e => e.EndDate)
            .HasComment("List price end date")
            .HasColumnType("datetime");
        entity.Property(e => e.ListPrice)
            .HasComment("Product list price.")
            .HasColumnType("money");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");

        entity.HasOne(d => d.Product).WithMany(p => p.ProductListPriceHistories)
            .HasForeignKey(d => d.ProductID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

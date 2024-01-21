using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ShoppingCartItemConfig : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> entity)
    {
        entity.HasKey(e => e.ShoppingCartItemID).HasName("PK_ShoppingCartItem_ShoppingCartItemID");

        entity.ToTable("ShoppingCartItem", "Sales", tb => tb.HasComment("Contains online customer orders until the order is submitted or cancelled."));

        entity.HasIndex(e => new { e.ShoppingCartID, e.ProductID }, "IX_ShoppingCartItem_ShoppingCartID_ProductID");

        entity.Property(e => e.ShoppingCartItemID).HasComment("Primary key for ShoppingCartItem records.");
        entity.Property(e => e.DateCreated)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date the time the record was created.")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.ProductID).HasComment("Product ordered. Foreign key to Product.ProductID.");
        entity.Property(e => e.Quantity)
            .HasDefaultValue(1)
            .HasComment("Product quantity ordered.");
        entity.Property(e => e.ShoppingCartID)
            .HasMaxLength(50)
            .HasComment("Shopping cart identification number.");

        entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCartItems)
            .HasForeignKey(d => d.ProductID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

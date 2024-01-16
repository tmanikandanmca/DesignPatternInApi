using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class SpecialOfferProductConfig : IEntityTypeConfiguration<SpecialOfferProduct>
{
    public void Configure(EntityTypeBuilder<SpecialOfferProduct> entity)
    {
        entity.HasKey(e => new { e.SpecialOfferID, e.ProductID }).HasName("PK_SpecialOfferProduct_SpecialOfferID_ProductID");

        entity.ToTable("SpecialOfferProduct", "Sales", tb => tb.HasComment("Cross-reference table mapping products to special offer discounts."));

        entity.HasIndex(e => e.rowguid, "AK_SpecialOfferProduct_rowguid").IsUnique();

        entity.HasIndex(e => e.ProductID, "IX_SpecialOfferProduct_ProductID");

        entity.Property(e => e.SpecialOfferID).HasComment("Primary key for SpecialOfferProduct records.");
        entity.Property(e => e.ProductID).HasComment("Product identification number. Foreign key to Product.ProductID.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.Product).WithMany(p => p.SpecialOfferProducts)
            .HasForeignKey(d => d.ProductID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.SpecialOffer).WithMany(p => p.SpecialOfferProducts)
            .HasForeignKey(d => d.SpecialOfferID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

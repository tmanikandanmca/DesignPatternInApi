using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductModelProductDescriptionCultureConfig : IEntityTypeConfiguration<ProductModelProductDescriptionCulture>
{
    public void Configure(EntityTypeBuilder<ProductModelProductDescriptionCulture> entity)
    {
        entity.HasKey(e => new { e.ProductModelID, e.ProductDescriptionID, e.CultureID }).HasName("PK_ProductModelProductDescriptionCulture_ProductModelID_ProductDescriptionID_CultureID");

        entity.ToTable("ProductModelProductDescriptionCulture", "Production", tb => tb.HasComment("Cross-reference table mapping product descriptions and the language the description is written in."));

        entity.Property(e => e.ProductModelID).HasComment("Primary key. Foreign key to ProductModel.ProductModelID.");
        entity.Property(e => e.ProductDescriptionID).HasComment("Primary key. Foreign key to ProductDescription.ProductDescriptionID.");
        entity.Property(e => e.CultureID)
            .HasMaxLength(6)
            .IsFixedLength()
            .HasComment("Culture identification number. Foreign key to Culture.CultureID.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");

        entity.HasOne(d => d.Culture).WithMany(p => p.ProductModelProductDescriptionCultures)
            .HasForeignKey(d => d.CultureID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.ProductDescription).WithMany(p => p.ProductModelProductDescriptionCultures)
            .HasForeignKey(d => d.ProductDescriptionID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.ProductModel).WithMany(p => p.ProductModelProductDescriptionCultures)
            .HasForeignKey(d => d.ProductModelID)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}

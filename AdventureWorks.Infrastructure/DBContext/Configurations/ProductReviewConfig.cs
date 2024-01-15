using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ProductReviewConfig : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> entity)
    {
        entity.HasKey(e => e.ProductReviewID).HasName("PK_ProductReview_ProductReviewID");

        entity.ToTable("ProductReview", "Production", tb => tb.HasComment("Customer reviews of products they have purchased."));

        entity.HasIndex(e => new { e.ProductID, e.ReviewerName }, "IX_ProductReview_ProductID_Name");

        entity.Property(e => e.ProductReviewID).HasComment("Primary key for ProductReview records.");
        entity.Property(e => e.Comments)
            .HasMaxLength(3850)
            .HasComment("Reviewer's comments");
        entity.Property(e => e.EmailAddress)
            .HasMaxLength(50)
            .HasComment("Reviewer's e-mail address.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.ProductID).HasComment("Product identification number. Foreign key to Product.ProductID.");
        entity.Property(e => e.Rating).HasComment("Product rating given by the reviewer. Scale is 1 to 5 with 5 as the highest rating.");
        entity.Property(e => e.ReviewDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date review was submitted.")
            .HasColumnType("datetime");
        entity.Property(e => e.ReviewerName)
            .HasMaxLength(50)
            .HasComment("Name of the reviewer.");

        entity.HasOne(d => d.Product).WithMany(p => p.ProductReviews)
            .HasForeignKey(d => d.ProductID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

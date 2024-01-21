using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class TransactionHistoryArchiveConfig : IEntityTypeConfiguration<TransactionHistoryArchive>
{
    public void Configure(EntityTypeBuilder<TransactionHistoryArchive> entity)
    {
        entity.HasKey(e => e.TransactionID).HasName("PK_TransactionHistoryArchive_TransactionID");

        entity.ToTable("TransactionHistoryArchive", "Production", tb => tb.HasComment("Transactions for previous years."));

        entity.HasIndex(e => e.ProductID, "IX_TransactionHistoryArchive_ProductID");

        entity.HasIndex(e => new { e.ReferenceOrderID, e.ReferenceOrderLineID }, "IX_TransactionHistoryArchive_ReferenceOrderID_ReferenceOrderLineID");

        entity.Property(e => e.TransactionID)
            .ValueGeneratedNever()
            .HasComment("Primary key for TransactionHistoryArchive records.");
        entity.Property(e => e.ActualCost)
            .HasComment("Product cost.")
            .HasColumnType("money");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.ProductID).HasComment("Product identification number. Foreign key to Product.ProductID.");
        entity.Property(e => e.Quantity).HasComment("Product quantity.");
        entity.Property(e => e.ReferenceOrderID).HasComment("Purchase order, sales order, or work order identification number.");
        entity.Property(e => e.ReferenceOrderLineID).HasComment("Line number associated with the purchase order, sales order, or work order.");
        entity.Property(e => e.TransactionDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time of the transaction.")
            .HasColumnType("datetime");
        entity.Property(e => e.TransactionType)
            .HasMaxLength(1)
            .IsFixedLength()
            .HasComment("W = Work Order, S = Sales Order, P = Purchase Order");

    }
}

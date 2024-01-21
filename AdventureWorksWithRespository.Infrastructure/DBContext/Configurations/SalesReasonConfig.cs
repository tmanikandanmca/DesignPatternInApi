using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class SalesReasonConfig : IEntityTypeConfiguration<SalesReason>
{
    public void Configure(EntityTypeBuilder<SalesReason> entity)
    {
        entity.HasKey(e => e.SalesReasonID).HasName("PK_SalesReason_SalesReasonID");

        entity.ToTable("SalesReason", "Sales", tb => tb.HasComment("Lookup table of customer purchase reasons."));

        entity.Property(e => e.SalesReasonID).HasComment("Primary key for SalesReason records.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Sales reason description.");
        entity.Property(e => e.ReasonType)
            .HasMaxLength(50)
            .HasComment("Category the sales reason belongs to.");

    }
}

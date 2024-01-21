using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ScrapReasonConfig : IEntityTypeConfiguration<ScrapReason>
{
    public void Configure(EntityTypeBuilder<ScrapReason> entity)
    {
        entity.HasKey(e => e.ScrapReasonID).HasName("PK_ScrapReason_ScrapReasonID");

        entity.ToTable("ScrapReason", "Production", tb => tb.HasComment("Manufacturing failure reasons lookup table."));

        entity.HasIndex(e => e.Name, "AK_ScrapReason_Name").IsUnique();

        entity.Property(e => e.ScrapReasonID).HasComment("Primary key for ScrapReason records.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Failure description.");

    }
}

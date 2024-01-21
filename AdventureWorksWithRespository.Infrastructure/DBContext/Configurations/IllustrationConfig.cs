using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class IllustrationConfig : IEntityTypeConfiguration<Illustration>
{
    public void Configure(EntityTypeBuilder<Illustration> entity)
    {
        entity.HasKey(e => e.IllustrationID).HasName("PK_Illustration_IllustrationID");

        entity.ToTable("Illustration", "Production", tb => tb.HasComment("Bicycle assembly diagrams."));

        entity.Property(e => e.IllustrationID).HasComment("Primary key for Illustration records.");
        entity.Property(e => e.Diagram)
            .HasComment("Illustrations used in manufacturing instructions. Stored as XML.")
            .HasColumnType("xml");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
    }
}

using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class CultureConfig : IEntityTypeConfiguration<Culture>
{
    public void Configure(EntityTypeBuilder<Culture> entity)
    {
        entity.HasKey(e => e.CultureID).HasName("PK_Culture_CultureID");

        entity.ToTable("Culture", "Production", tb => tb.HasComment("Lookup table containing the languages in which some AdventureWorks data is stored."));

        entity.HasIndex(e => e.Name, "AK_Culture_Name").IsUnique();

        entity.Property(e => e.CultureID)
            .HasMaxLength(6)
            .IsFixedLength()
            .HasComment("Primary key for Culture records.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Culture description.");
    }
}

using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class UnitMeasureConfig : IEntityTypeConfiguration<UnitMeasure>
{
    public void Configure(EntityTypeBuilder<UnitMeasure> entity)
    {
        entity.HasKey(e => e.UnitMeasureCode).HasName("PK_UnitMeasure_UnitMeasureCode");

        entity.ToTable("UnitMeasure", "Production", tb => tb.HasComment("Unit of measure lookup table."));

        entity.HasIndex(e => e.Name, "AK_UnitMeasure_Name").IsUnique();

        entity.Property(e => e.UnitMeasureCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasComment("Primary key.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Unit of measure description.");
    }
}

using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ShiftConfig : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> entity)
    {
        entity.HasKey(e => e.ShiftID).HasName("PK_Shift_ShiftID");

        entity.ToTable("Shift", "HumanResources", tb => tb.HasComment("Work shift lookup table."));

        entity.HasIndex(e => e.Name, "AK_Shift_Name").IsUnique();

        entity.HasIndex(e => new { e.StartTime, e.EndTime }, "AK_Shift_StartTime_EndTime").IsUnique();

        entity.Property(e => e.ShiftID)
            .ValueGeneratedOnAdd()
            .HasComment("Primary key for Shift records.");
        entity.Property(e => e.EndTime).HasComment("Shift end time.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Shift description.");
        entity.Property(e => e.StartTime).HasComment("Shift start time.");

    }
}

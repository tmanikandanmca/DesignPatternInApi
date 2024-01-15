using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class LocationConfig : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> entity)
    {
        entity.HasKey(e => e.LocationID).HasName("PK_Location_LocationID");

        entity.ToTable("Location", "Production", tb => tb.HasComment("Product inventory and manufacturing locations."));

        entity.HasIndex(e => e.Name, "AK_Location_Name").IsUnique();

        entity.Property(e => e.LocationID).HasComment("Primary key for Location records.");
        entity.Property(e => e.Availability)
            .HasComment("Work capacity (in hours) of the manufacturing location.")
            .HasColumnType("decimal(8, 2)");
        entity.Property(e => e.CostRate)
            .HasComment("Standard hourly cost of the manufacturing location.")
            .HasColumnType("smallmoney");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Location description.");
    }
}

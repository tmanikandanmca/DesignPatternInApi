using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations
{
    internal class CountryRegionConfig : IEntityTypeConfiguration<CountryRegion>
    {
        public void Configure(EntityTypeBuilder<CountryRegion> entity)
        {
            entity.HasKey(e => e.CountryRegionCode).HasName("PK_CountryRegion_CountryRegionCode");

            entity.ToTable("CountryRegion", "Person", tb => tb.HasComment("Lookup table containing the ISO standard codes for countries and regions."));

            entity.HasIndex(e => e.Name, "AK_CountryRegion_Name").IsUnique();

            entity.Property(e => e.CountryRegionCode)
                .HasMaxLength(3)
                .HasComment("ISO standard code for countries and regions.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Country or region name.");
        }
    }
}

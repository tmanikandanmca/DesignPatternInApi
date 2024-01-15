using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class AWBuildVersionConfig : IEntityTypeConfiguration<AWBuildVersion>
{
    public void Configure(EntityTypeBuilder<AWBuildVersion> entity)
    {
        entity.HasKey(e => e.SystemInformationID).HasName("PK_AWBuildVersion_SystemInformationID");

        entity.ToTable("AWBuildVersion", tb => tb.HasComment("Current version number of the AdventureWorks 2016 sample database. "));

        entity.Property(e => e.SystemInformationID)
            .ValueGeneratedOnAdd()
            .HasComment("Primary key for AWBuildVersion records.");
        entity.Property(e => e.Database_Version)
            .HasMaxLength(25)
            .HasComment("Version number of the database in 9.yy.mm.dd.00 format.")
            .HasColumnName("Database Version");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.VersionDate)
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime"); ;
    }
}

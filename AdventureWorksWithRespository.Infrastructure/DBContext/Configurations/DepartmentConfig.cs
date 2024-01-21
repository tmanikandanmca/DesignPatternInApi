using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class DepartmentConfig : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> entity)
    {
        entity.HasKey(e => e.DepartmentID).HasName("PK_Department_DepartmentID");

        entity.ToTable("Department", "HumanResources", tb => tb.HasComment("Lookup table containing the departments within the Adventure Works Cycles company."));

        entity.HasIndex(e => e.Name, "AK_Department_Name").IsUnique();

        entity.Property(e => e.DepartmentID).HasComment("Primary key for Department records.");
        entity.Property(e => e.GroupName)
            .HasMaxLength(50)
            .HasComment("Name of the group to which the department belongs.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Name of the department.");
    }
}

using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class EmployeeDepartmentHistoryConfig : IEntityTypeConfiguration<EmployeeDepartmentHistory>
{
    public void Configure(EntityTypeBuilder<EmployeeDepartmentHistory> entity)
    {
        entity.HasKey(e => new { e.BusinessEntityID, e.StartDate, e.DepartmentID, e.ShiftID }).HasName("PK_EmployeeDepartmentHistory_BusinessEntityID_StartDate_DepartmentID");

        entity.ToTable("EmployeeDepartmentHistory", "HumanResources", tb => tb.HasComment("Employee department transfers."));

        entity.HasIndex(e => e.DepartmentID, "IX_EmployeeDepartmentHistory_DepartmentID");

        entity.HasIndex(e => e.ShiftID, "IX_EmployeeDepartmentHistory_ShiftID");

        entity.Property(e => e.BusinessEntityID).HasComment("Employee identification number. Foreign key to Employee.BusinessEntityID.");
        entity.Property(e => e.StartDate).HasComment("Date the employee started work in the department.");
        entity.Property(e => e.DepartmentID).HasComment("Department in which the employee worked including currently. Foreign key to Department.DepartmentID.");
        entity.Property(e => e.ShiftID).HasComment("Identifies which 8-hour shift the employee works. Foreign key to Shift.Shift.ID.");
        entity.Property(e => e.EndDate).HasComment("Date the employee left the department. NULL = Current department.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");

        entity.HasOne(d => d.BusinessEntity).WithMany(p => p.EmployeeDepartmentHistories)
            .HasForeignKey(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Department).WithMany(p => p.EmployeeDepartmentHistories)
            .HasForeignKey(d => d.DepartmentID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Shift).WithMany(p => p.EmployeeDepartmentHistories)
            .HasForeignKey(d => d.ShiftID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

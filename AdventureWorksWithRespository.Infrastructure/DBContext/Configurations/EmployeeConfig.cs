using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> entity)
    {
        entity.HasKey(e => e.BusinessEntityID).HasName("PK_Employee_BusinessEntityID");

        entity.ToTable("Employee", "HumanResources", tb =>
        {
            tb.HasComment("Employee information such as salary, department, and title.");
            tb.HasTrigger("dEmployee");
        });

        entity.HasIndex(e => e.LoginID, "AK_Employee_LoginID").IsUnique();

        entity.HasIndex(e => e.NationalIDNumber, "AK_Employee_NationalIDNumber").IsUnique();

        entity.HasIndex(e => e.rowguid, "AK_Employee_rowguid").IsUnique();

        entity.Property(e => e.BusinessEntityID)
            .ValueGeneratedNever()
            .HasComment("Primary key for Employee records.  Foreign key to BusinessEntity.BusinessEntityID.");
        entity.Property(e => e.BirthDate).HasComment("Date of birth.");
        entity.Property(e => e.CurrentFlag)
            .HasDefaultValue(true)
            .HasComment("0 = Inactive, 1 = Active");
        entity.Property(e => e.Gender)
            .HasMaxLength(1)
            .IsFixedLength()
            .HasComment("M = Male, F = Female");
        entity.Property(e => e.HireDate).HasComment("Employee hired on this date.");
        entity.Property(e => e.JobTitle)
            .HasMaxLength(50)
            .HasComment("Work title such as Buyer or Sales Representative.");
        entity.Property(e => e.LoginID)
            .HasMaxLength(256)
            .HasComment("Network login.");
        entity.Property(e => e.MaritalStatus)
            .HasMaxLength(1)
            .IsFixedLength()
            .HasComment("M = Married, S = Single");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.NationalIDNumber)
            .HasMaxLength(15)
            .HasComment("Unique national identification number such as a social security number.");
        entity.Property(e => e.OrganizationLevel)
            .HasComputedColumnSql("([OrganizationNode].[GetLevel]())", false)
            .HasComment("The depth of the employee in the corporate hierarchy.");
        entity.Property(e => e.SalariedFlag)
            .HasDefaultValue(true)
            .HasComment("Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective bargaining.");
        entity.Property(e => e.SickLeaveHours).HasComment("Number of available sick leave hours.");
        entity.Property(e => e.VacationHours).HasComment("Number of available vacation hours.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.BusinessEntity).WithOne(p => p.Employee)
            .HasForeignKey<Employee>(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

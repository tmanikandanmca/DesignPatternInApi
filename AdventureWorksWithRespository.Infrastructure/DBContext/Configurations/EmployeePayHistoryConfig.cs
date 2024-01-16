using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class EmployeePayHistoryConfig : IEntityTypeConfiguration<EmployeePayHistory>
{
    public void Configure(EntityTypeBuilder<EmployeePayHistory> entity)
    {
        entity.HasKey(e => new { e.BusinessEntityID, e.RateChangeDate }).HasName("PK_EmployeePayHistory_BusinessEntityID_RateChangeDate");

        entity.ToTable("EmployeePayHistory", "HumanResources", tb => tb.HasComment("Employee pay history."));

        entity.Property(e => e.BusinessEntityID).HasComment("Employee identification number. Foreign key to Employee.BusinessEntityID.");
        entity.Property(e => e.RateChangeDate)
            .HasComment("Date the change in pay is effective")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.PayFrequency).HasComment("1 = Salary received monthly, 2 = Salary received biweekly");
        entity.Property(e => e.Rate)
            .HasComment("Salary hourly rate.")
            .HasColumnType("money");

        entity.HasOne(d => d.BusinessEntity).WithMany(p => p.EmployeePayHistories)
            .HasForeignKey(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

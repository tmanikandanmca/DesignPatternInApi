using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class SalesPersonConfig : IEntityTypeConfiguration<SalesPerson>
{
    public void Configure(EntityTypeBuilder<SalesPerson> entity)
    {
        entity.HasKey(e => e.BusinessEntityID).HasName("PK_SalesPerson_BusinessEntityID");

        entity.ToTable("SalesPerson", "Sales", tb => tb.HasComment("Sales representative current information."));

        entity.HasIndex(e => e.rowguid, "AK_SalesPerson_rowguid").IsUnique();

        entity.Property(e => e.BusinessEntityID)
            .ValueGeneratedNever()
            .HasComment("Primary key for SalesPerson records. Foreign key to Employee.BusinessEntityID");
        entity.Property(e => e.Bonus)
            .HasComment("Bonus due if quota is met.")
            .HasColumnType("money");
        entity.Property(e => e.CommissionPct)
            .HasComment("Commision percent received per sale.")
            .HasColumnType("smallmoney");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.SalesLastYear)
            .HasComment("Sales total of previous year.")
            .HasColumnType("money");
        entity.Property(e => e.SalesQuota)
            .HasComment("Projected yearly sales.")
            .HasColumnType("money");
        entity.Property(e => e.SalesYTD)
            .HasComment("Sales total year to date.")
            .HasColumnType("money");
        entity.Property(e => e.TerritoryID).HasComment("Territory currently assigned to. Foreign key to SalesTerritory.SalesTerritoryID.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.BusinessEntity).WithOne(p => p.SalesPerson)
            .HasForeignKey<SalesPerson>(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Territory).WithMany(p => p.SalesPeople).HasForeignKey(d => d.TerritoryID);

    }
}

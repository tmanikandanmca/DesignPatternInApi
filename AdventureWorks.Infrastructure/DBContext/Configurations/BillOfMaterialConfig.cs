using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class BillOfMaterialConfiguration : IEntityTypeConfiguration<BillOfMaterial>
{
    public void Configure(EntityTypeBuilder<BillOfMaterial> entity)
    {
        entity.HasKey(e => e.BillOfMaterialsID)
                 .HasName("PK_BillOfMaterials_BillOfMaterialsID")
                 .IsClustered(false);

        entity.ToTable("BillOfMaterials", "Production", tb => tb.HasComment("Items required to make bicycles and bicycle subassemblies. It identifies the heirarchical relationship between a parent product and its components."));

        entity.HasIndex(e => new { e.ProductAssemblyID, e.ComponentID, e.StartDate }, "AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate")
            .IsUnique()
            .IsClustered();

        entity.HasIndex(e => e.UnitMeasureCode, "IX_BillOfMaterials_UnitMeasureCode");

        entity.Property(e => e.BillOfMaterialsID).HasComment("Primary key for BillOfMaterials records.");
        entity.Property(e => e.BOMLevel).HasComment("Indicates the depth the component is from its parent (AssemblyID).");
        entity.Property(e => e.ComponentID).HasComment("Component identification number. Foreign key to Product.ProductID.");
        entity.Property(e => e.EndDate)
            .HasComment("Date the component stopped being used in the assembly item.")
            .HasColumnType("datetime");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.PerAssemblyQty)
            .HasDefaultValue(1.00m)
            .HasComment("Quantity of the component needed to create the assembly.")
            .HasColumnType("decimal(8, 2)");
        entity.Property(e => e.ProductAssemblyID).HasComment("Parent product identification number. Foreign key to Product.ProductID.");
        entity.Property(e => e.StartDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date the component started being used in the assembly item.")
            .HasColumnType("datetime");
        entity.Property(e => e.UnitMeasureCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .HasComment("Standard code identifying the unit of measure for the quantity.");

        entity.HasOne(d => d.Component).WithMany(p => p.BillOfMaterialComponents)
            .HasForeignKey(d => d.ComponentID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.ProductAssembly).WithMany(p => p.BillOfMaterialProductAssemblies).HasForeignKey(d => d.ProductAssemblyID);

        entity.HasOne(d => d.UnitMeasureCodeNavigation).WithMany(p => p.BillOfMaterials)
            .HasForeignKey(d => d.UnitMeasureCode)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

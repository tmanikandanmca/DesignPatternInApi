using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations
{
    internal class BusinessEntityContactConfig : IEntityTypeConfiguration<BusinessEntityContact>
    {
        public void Configure(EntityTypeBuilder<BusinessEntityContact> entity)
        {
            entity.HasKey(e => new { e.BusinessEntityID, e.PersonID, e.ContactTypeID }).HasName("PK_BusinessEntityContact_BusinessEntityID_PersonID_ContactTypeID");

            entity.ToTable("BusinessEntityContact", "Person", tb => tb.HasComment("Cross-reference table mapping stores, vendors, and employees to people"));

            entity.HasIndex(e => e.rowguid, "AK_BusinessEntityContact_rowguid").IsUnique();

            entity.HasIndex(e => e.ContactTypeID, "IX_BusinessEntityContact_ContactTypeID");

            entity.HasIndex(e => e.PersonID, "IX_BusinessEntityContact_PersonID");

            entity.Property(e => e.BusinessEntityID).HasComment("Primary key. Foreign key to BusinessEntity.BusinessEntityID.");
            entity.Property(e => e.PersonID).HasComment("Primary key. Foreign key to Person.BusinessEntityID.");
            entity.Property(e => e.ContactTypeID).HasComment("Primary key.  Foreign key to ContactType.ContactTypeID.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.BusinessEntity).WithMany(p => p.BusinessEntityContacts)
                .HasForeignKey(d => d.BusinessEntityID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ContactType).WithMany(p => p.BusinessEntityContacts)
                .HasForeignKey(d => d.ContactTypeID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Person).WithMany(p => p.BusinessEntityContacts)
                .HasForeignKey(d => d.PersonID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}

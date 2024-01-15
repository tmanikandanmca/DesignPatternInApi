using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class EmailAddressConfig : IEntityTypeConfiguration<EmailAddress>
{
    public void Configure(EntityTypeBuilder<EmailAddress> entity)
    {
        entity.HasKey(e => new { e.BusinessEntityID, e.EmailAddressID }).HasName("PK_EmailAddress_BusinessEntityID_EmailAddressID");

        entity.ToTable("EmailAddress", "Person", tb => tb.HasComment("Where to send a person email."));

        entity.HasIndex(e => e.EmailAddress1, "IX_EmailAddress_EmailAddress");

        entity.Property(e => e.BusinessEntityID).HasComment("Primary key. Person associated with this email address.  Foreign key to Person.BusinessEntityID");
        entity.Property(e => e.EmailAddressID)
            .ValueGeneratedOnAdd()
            .HasComment("Primary key. ID of this email address.");
        entity.Property(e => e.EmailAddress1)
            .HasMaxLength(50)
            .HasComment("E-mail address for the person.")
            .HasColumnName("EmailAddress");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.BusinessEntity).WithMany(p => p.EmailAddresses)
            .HasForeignKey(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

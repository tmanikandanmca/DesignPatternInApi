using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class PersonPhoneConfig : IEntityTypeConfiguration<PersonPhone>
{
    public void Configure(EntityTypeBuilder<PersonPhone> entity)
    {
        entity.HasKey(e => new { e.BusinessEntityID, e.PhoneNumber, e.PhoneNumberTypeID }).HasName("PK_PersonPhone_BusinessEntityID_PhoneNumber_PhoneNumberTypeID");

        entity.ToTable("PersonPhone", "Person", tb => tb.HasComment("Telephone number and type of a person."));

        entity.HasIndex(e => e.PhoneNumber, "IX_PersonPhone_PhoneNumber");

        entity.Property(e => e.BusinessEntityID).HasComment("Business entity identification number. Foreign key to Person.BusinessEntityID.");
        entity.Property(e => e.PhoneNumber)
            .HasMaxLength(25)
            .HasComment("Telephone number identification number.");
        entity.Property(e => e.PhoneNumberTypeID).HasComment("Kind of phone number. Foreign key to PhoneNumberType.PhoneNumberTypeID.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");

        entity.HasOne(d => d.BusinessEntity).WithMany(p => p.PersonPhones)
            .HasForeignKey(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.PhoneNumberType).WithMany(p => p.PersonPhones)
            .HasForeignKey(d => d.PhoneNumberTypeID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

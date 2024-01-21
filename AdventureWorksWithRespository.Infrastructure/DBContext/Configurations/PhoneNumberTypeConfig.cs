using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class PhoneNumberTypeConfig : IEntityTypeConfiguration<PhoneNumberType>
{
    public void Configure(EntityTypeBuilder<PhoneNumberType> entity)
    {
        entity.HasKey(e => e.PhoneNumberTypeID).HasName("PK_PhoneNumberType_PhoneNumberTypeID");

        entity.ToTable("PhoneNumberType", "Person", tb => tb.HasComment("Type of phone number of a person."));

        entity.Property(e => e.PhoneNumberTypeID).HasComment("Primary key for telephone number type records.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Name of the telephone number type");
    }
}

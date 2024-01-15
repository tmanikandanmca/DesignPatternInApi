using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class PersonCreditCardConfig : IEntityTypeConfiguration<PersonCreditCard>
{
    public void Configure(EntityTypeBuilder<PersonCreditCard> entity)
    {
        entity.HasKey(e => new { e.BusinessEntityID, e.CreditCardID }).HasName("PK_PersonCreditCard_BusinessEntityID_CreditCardID");

        entity.ToTable("PersonCreditCard", "Sales", tb => tb.HasComment("Cross-reference table mapping people to their credit card information in the CreditCard table. "));

        entity.Property(e => e.BusinessEntityID).HasComment("Business entity identification number. Foreign key to Person.BusinessEntityID.");
        entity.Property(e => e.CreditCardID).HasComment("Credit card identification number. Foreign key to CreditCard.CreditCardID.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");

        entity.HasOne(d => d.BusinessEntity).WithMany(p => p.PersonCreditCards)
            .HasForeignKey(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.CreditCard).WithMany(p => p.PersonCreditCards)
            .HasForeignKey(d => d.CreditCardID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

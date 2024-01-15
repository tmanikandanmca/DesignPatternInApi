using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
{
    public void Configure(EntityTypeBuilder<CreditCard> entity)
    {
        entity.HasKey(e => e.CreditCardID).HasName("PK_CreditCard_CreditCardID");

        entity.ToTable("CreditCard", "Sales", tb => tb.HasComment("Customer credit card information."));

        entity.HasIndex(e => e.CardNumber, "AK_CreditCard_CardNumber").IsUnique();

        entity.Property(e => e.CreditCardID).HasComment("Primary key for CreditCard records.");
        entity.Property(e => e.CardNumber)
            .HasMaxLength(25)
            .HasComment("Credit card number.");
        entity.Property(e => e.CardType)
            .HasMaxLength(50)
            .HasComment("Credit card name.");
        entity.Property(e => e.ExpMonth).HasComment("Credit card expiration month.");
        entity.Property(e => e.ExpYear).HasComment("Credit card expiration year.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
    }
}

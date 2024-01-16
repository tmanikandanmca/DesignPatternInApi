using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ContactTypeConfig : IEntityTypeConfiguration<ContactType>
{
    public void Configure(EntityTypeBuilder<ContactType> entity)
    {
        entity.HasKey(e => e.ContactTypeID).HasName("PK_ContactType_ContactTypeID");

        entity.ToTable("ContactType", "Person", tb => tb.HasComment("Lookup table containing the types of business entity contacts."));

        entity.HasIndex(e => e.Name, "AK_ContactType_Name").IsUnique();

        entity.Property(e => e.ContactTypeID).HasComment("Primary key for ContactType records.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasComment("Contact type description.");
    }
}

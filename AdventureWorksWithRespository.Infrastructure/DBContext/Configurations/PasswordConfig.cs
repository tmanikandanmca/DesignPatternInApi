using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class PasswordConfig : IEntityTypeConfiguration<Password>
{
    public void Configure(EntityTypeBuilder<Password> entity)
    {
        entity.HasKey(e => e.BusinessEntityID).HasName("PK_Password_BusinessEntityID");

        entity.ToTable("Password", "Person", tb => tb.HasComment("One way hashed authentication information"));

        entity.Property(e => e.BusinessEntityID).ValueGeneratedNever();
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.PasswordHash)
            .HasMaxLength(128)
            .IsUnicode(false)
            .HasComment("Password for the e-mail account.");
        entity.Property(e => e.PasswordSalt)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasComment("Random value concatenated with the password string before the password is hashed.");
        entity.Property(e => e.rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.HasOne(d => d.BusinessEntity).WithOne(p => p.Password)
            .HasForeignKey<Password>(d => d.BusinessEntityID)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

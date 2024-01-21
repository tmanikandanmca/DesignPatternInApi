using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class ErrorLogConfig : IEntityTypeConfiguration<ErrorLog>
{
    public void Configure(EntityTypeBuilder<ErrorLog> entity)
    {
        entity.HasKey(e => e.ErrorLogID).HasName("PK_ErrorLog_ErrorLogID");

        entity.ToTable("ErrorLog", tb => tb.HasComment("Audit table tracking errors in the the AdventureWorks database that are caught by the CATCH block of a TRY...CATCH construct. Data is inserted by stored procedure dbo.uspLogError when it is executed from inside the CATCH block of a TRY...CATCH construct."));

        entity.Property(e => e.ErrorLogID).HasComment("Primary key for ErrorLog records.");
        entity.Property(e => e.ErrorLine).HasComment("The line number at which the error occurred.");
        entity.Property(e => e.ErrorMessage)
            .HasMaxLength(4000)
            .HasComment("The message text of the error that occurred.");
        entity.Property(e => e.ErrorNumber).HasComment("The error number of the error that occurred.");
        entity.Property(e => e.ErrorProcedure)
            .HasMaxLength(126)
            .HasComment("The name of the stored procedure or trigger where the error occurred.");
        entity.Property(e => e.ErrorSeverity).HasComment("The severity of the error that occurred.");
        entity.Property(e => e.ErrorState).HasComment("The state number of the error that occurred.");
        entity.Property(e => e.ErrorTime)
            .HasDefaultValueSql("(getdate())")
            .HasComment("The date and time at which the error occurred.")
            .HasColumnType("datetime");
        entity.Property(e => e.UserName)
            .HasMaxLength(128)
            .HasComment("The user who executed the batch in which the error occurred.");
    }
}

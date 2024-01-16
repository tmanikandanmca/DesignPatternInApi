using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class JobCandidateConfig : IEntityTypeConfiguration<JobCandidate>
{
    public void Configure(EntityTypeBuilder<JobCandidate> entity)
    {
        entity.HasKey(e => e.JobCandidateID).HasName("PK_JobCandidate_JobCandidateID");

        entity.ToTable("JobCandidate", "HumanResources", tb => tb.HasComment("Résumés submitted to Human Resources by job applicants."));

        entity.HasIndex(e => e.BusinessEntityID, "IX_JobCandidate_BusinessEntityID");

        entity.Property(e => e.JobCandidateID).HasComment("Primary key for JobCandidate records.");
        entity.Property(e => e.BusinessEntityID).HasComment("Employee identification number if applicant was hired. Foreign key to Employee.BusinessEntityID.");
        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.")
            .HasColumnType("datetime");
        entity.Property(e => e.Resume)
            .HasComment("Résumé in XML format.")
            .HasColumnType("xml");

        entity.HasOne(d => d.BusinessEntity).WithMany(p => p.JobCandidates).HasForeignKey(d => d.BusinessEntityID);
    }
}

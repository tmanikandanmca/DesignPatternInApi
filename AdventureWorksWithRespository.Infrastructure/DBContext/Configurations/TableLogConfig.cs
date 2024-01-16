using AdventureWorks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorks.Infrastructure.DBContext.Configurations;

internal class TableLogConfig : IEntityTypeConfiguration<TableLog>
{
    public void Configure(EntityTypeBuilder<TableLog> entity)
    {
        entity.HasKey(e => e.LogID).HasName("PK__TableLog__5E5499A81B51B565");

        entity.ToTable("TableLog");

        entity.Property(e => e.ChangedBy).HasMaxLength(128);
        entity.Property(e => e.EventDate).HasColumnType("datetime");
        entity.Property(e => e.EventVal).HasColumnType("xml");
    }
}

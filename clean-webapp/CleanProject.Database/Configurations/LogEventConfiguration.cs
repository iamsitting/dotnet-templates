using CleanProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProject.Database.Configurations;

internal sealed class LogEventConfiguration : IEntityTypeConfiguration<LogEvent>
{
    private readonly IColumnTypes _types;

    public LogEventConfiguration(IColumnTypes types)
    {
        _types = types;
    }

    public void Configure(EntityTypeBuilder<LogEvent> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Message)
            .HasColumnType(_types.String());
        builder.Property(x => x.MessageTemplate)
            .HasColumnType(_types.String());
        builder.Property(x => x.Level)
            .HasColumnType(_types.String(128));
        builder.Property(x => x.Timestamp)
            .HasColumnType(_types.String());
        builder.Property(x => x.Exception)
            .HasColumnType(_types.String());
        builder.Property(x => x.Properties)
            .HasColumnType(_types.String());

        builder.Property(x => x.CreatedOn)
            .HasColumnType(_types.Timestamp());
        builder.Property(x => x.ArchivedOn)
            .HasColumnType(_types.Timestamp());
    }
}
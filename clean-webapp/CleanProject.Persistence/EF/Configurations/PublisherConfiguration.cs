using CleanProject.Persistence.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProject.Persistence.EF.Configurations;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    private readonly IColumnTypes _types;

    public PublisherConfiguration(IColumnTypes types)
    {
        _types = types;
    }

    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedOn)
            .HasColumnType(_types.Timestamp());
        
        builder.Property(x => x.ArchivedOn)
            .HasColumnType(_types.Timestamp());
        builder.Property(x => x.Name)
            .HasColumnType(_types.String());
    }
}
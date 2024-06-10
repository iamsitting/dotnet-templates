using CleanProject.Persistence.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProject.Persistence.EF.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    private readonly IColumnTypes _types;

    public AuthorConfiguration(IColumnTypes types)
    {
        _types = types;
    }

    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedOn)
            .HasColumnType(_types.Timestamp());
        
        builder.Property(x => x.ArchivedOn)
            .HasColumnType(_types.Timestamp());
        builder.Property(x => x.FirstName)
            .HasColumnType(_types.String());
        builder.Property(x => x.LastName)
            .HasColumnType(_types.String());
        builder.Property(x => x.DateOfBirth)
            .HasColumnType(_types.Date());
    }
}
using CleanProject.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProject.Database.Configurations;

internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    private readonly IColumnTypes _types;

    public BookConfiguration(IColumnTypes types)
    {
        _types = types;
    }

    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ArchivedOn)
            .HasColumnType(_types.Timestamp());

        builder.Property(x => x.CreatedOn)
            .HasColumnType(_types.Timestamp());

        builder.Property(x => x.Author)
            .HasColumnType(_types.String(100));

        builder.Property(x => x.Title)
            .HasColumnType(_types.String(200));
    }
}
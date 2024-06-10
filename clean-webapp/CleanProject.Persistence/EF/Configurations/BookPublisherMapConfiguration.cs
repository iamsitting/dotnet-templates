using CleanProject.Persistence.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProject.Persistence.EF.Configurations;

public class BookPublisherMapConfiguration : IEntityTypeConfiguration<BookPublisherMap>
{
    public void Configure(EntityTypeBuilder<BookPublisherMap> builder)
    {
        builder.HasKey(x => new { x.BookId, x.PublisherId });

        builder.HasOne(x => x.Book)
            .WithMany(x => x.BookPublisherMaps)
            .HasForeignKey(x => x.BookId);

        builder.HasOne(x => x.Publisher)
            .WithMany()
            .HasForeignKey(x => x.PublisherId);
    }
}
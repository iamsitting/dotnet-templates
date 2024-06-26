using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateProject.Entities.Identity;

namespace TemplateProject.Database.Configurations;

internal sealed class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    private readonly IColumnTypes _types;

    public AppRoleConfiguration(IColumnTypes types)
    {
        _types = types;
    }

    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.CreatedOn)
            .HasColumnType(_types.Timestamp());
        
        builder.Property(x => x.ArchivedOn)
            .HasColumnType(_types.Timestamp());
    }
}
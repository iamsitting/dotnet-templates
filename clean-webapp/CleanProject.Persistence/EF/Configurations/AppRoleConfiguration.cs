using CleanProject.Persistence.EF.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProject.Persistence.EF.Configurations;

internal sealed class AppRoleConfiguration : IEntityTypeConfiguration<Role>
{
    private readonly IColumnTypes _types;

    public AppRoleConfiguration(IColumnTypes types)
    {
        _types = types;
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.CreatedOn)
            .HasColumnType(_types.Timestamp());
        
        builder.Property(x => x.ArchivedOn)
            .HasColumnType(_types.Timestamp());
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TemplateProject.Entities.Identity;

namespace TemplateProject.Database.Configurations;

internal sealed class UserRoleMapConfiguration : IEntityTypeConfiguration<UserRoleMap>
{
    private readonly IColumnTypes _types;

    public UserRoleMapConfiguration(IColumnTypes types)
    {
        _types = types;
    }

    public void Configure(EntityTypeBuilder<Entities.Identity.UserRoleMap> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.UserId });
        
        builder.Property(x => x.CreatedOn)
            .HasColumnType(_types.Timestamp());
        builder.Property(x => x.ArchivedOn)
            .HasColumnType(_types.Timestamp());

        builder.HasOne(x => x.AppRole)
            .WithMany()
            .HasForeignKey(x => x.RoleId);

        builder.HasOne(x => x.AppUser)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}
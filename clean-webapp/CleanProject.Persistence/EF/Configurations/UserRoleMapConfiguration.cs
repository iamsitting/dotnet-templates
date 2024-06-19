using CleanProject.Persistence.EF.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanProject.Persistence.EF.Configurations;

internal sealed class UserRoleMapConfiguration : IEntityTypeConfiguration<UserRoleMap>
{
    private readonly IColumnTypes _types;

    public UserRoleMapConfiguration(IColumnTypes types)
    {
        _types = types;
    }

    public void Configure(EntityTypeBuilder<UserRoleMap> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.UserId });
        
        builder.Property(x => x.CreatedOn)
            .HasColumnType(_types.Timestamp());
        builder.Property(x => x.ArchivedOn)
            .HasColumnType(_types.Timestamp());

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}
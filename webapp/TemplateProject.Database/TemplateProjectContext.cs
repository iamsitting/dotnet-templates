using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TemplateProject.Database.Configurations;
using TemplateProject.Entities.Identity;

namespace TemplateProject.Database;

public class TemplateProjectContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, UserRoleMap, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly IColumnTypes _types;

    public TemplateProjectContext(DbContextOptions<TemplateProjectContext> options, IColumnTypes types) : base(options)
    {
        _types = types;
    }

    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<AppRole> AppRoles => Set<AppRole>();
    public DbSet<UserRoleMap> UserRoleMaps => Set<UserRoleMap>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new AppUserConfiguration(_types));
        builder.ApplyConfiguration(new AppRoleConfiguration(_types));
    }



}
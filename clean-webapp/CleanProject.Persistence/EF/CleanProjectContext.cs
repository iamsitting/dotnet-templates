using CleanProject.Persistence.EF.Configurations;
using CleanProject.Persistence.EF.Entities;
using CleanProject.Persistence.EF.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Persistence.EF;

public sealed class CleanProjectContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, UserRoleMap, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private readonly IColumnTypes _types;

    public CleanProjectContext(DbContextOptions<CleanProjectContext> options, IColumnTypes types) : base(options)
    {
        _types = types;
    }

    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<AppRole> AppRoles => Set<AppRole>();
    public DbSet<UserRoleMap> UserRoleMaps => Set<UserRoleMap>();
    public DbSet<LogEvent> LogEvents => Set<LogEvent>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Publisher> Publishers => Set<Publisher>();
    public DbSet<BookPublisherMap> BookPublisherMaps => Set<BookPublisherMap>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new AppUserConfiguration(_types));
        builder.ApplyConfiguration(new AppRoleConfiguration(_types));
        builder.ApplyConfiguration(new UserRoleMapConfiguration(_types));
        builder.ApplyConfiguration(new LogEventConfiguration(_types));
        builder.ApplyConfiguration(new BookConfiguration(_types));
    }
    
}
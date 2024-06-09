using Microsoft.AspNetCore.Identity;

namespace CleanProject.Database.Entities.Identity;

public sealed class UserRoleMap : IdentityUserRole<Guid>, IBaseEntity
{
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
    public AppRole AppRole { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
}
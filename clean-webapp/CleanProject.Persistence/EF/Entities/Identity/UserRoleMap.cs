using Microsoft.AspNetCore.Identity;

namespace CleanProject.Persistence.EF.Entities.Identity;

public sealed class UserRoleMap : IdentityUserRole<Guid>
{
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
    public Role Role { get; set; } = null!;
    public User User { get; set; } = null!;
}
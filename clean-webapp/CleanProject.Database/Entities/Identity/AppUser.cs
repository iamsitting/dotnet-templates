using Microsoft.AspNetCore.Identity;

namespace CleanProject.Database.Entities.Identity;

public sealed class AppUser : IdentityUser<Guid>, IEntity
{
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
}
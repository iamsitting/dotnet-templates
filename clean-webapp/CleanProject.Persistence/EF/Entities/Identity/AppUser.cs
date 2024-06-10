using CleanProject.CoreApplication.Domain;
using Microsoft.AspNetCore.Identity;

namespace CleanProject.Persistence.EF.Entities.Identity;

public sealed class AppUser : IdentityUser<Guid>, IUser, IWithKey<Guid>
{
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
}
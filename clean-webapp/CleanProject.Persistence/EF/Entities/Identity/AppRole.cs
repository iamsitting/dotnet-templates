using CleanProject.CoreApplication.Domain;
using Microsoft.AspNetCore.Identity;

namespace CleanProject.Persistence.EF.Entities.Identity;

public sealed class AppRole : IdentityRole<Guid>, IRole, IWithKey<Guid>
{
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
}
using Microsoft.AspNetCore.Identity;

namespace TemplateProject.Entities.Identity;

public sealed class AppRole : IdentityRole<Guid>, IEntity
{
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
}
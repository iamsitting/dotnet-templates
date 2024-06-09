using CleanProject.CoreApplication.Database;

namespace CleanProject.CoreApplication.Domain;

public class AppUser : IEntity
{
    public string Email { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
    public Guid Id { get; set; }
}
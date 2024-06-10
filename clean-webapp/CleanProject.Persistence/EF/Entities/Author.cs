using CleanProject.CoreApplication.Domain;

namespace CleanProject.Persistence.EF.Entities;

public sealed class Author : IWithKey<Guid>, IBaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public Guid Id { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
    public string FullName() => FirstName + " " + LastName;
}
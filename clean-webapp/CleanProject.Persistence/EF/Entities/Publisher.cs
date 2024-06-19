namespace CleanProject.Persistence.EF.Entities;

public sealed class Publisher
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
}
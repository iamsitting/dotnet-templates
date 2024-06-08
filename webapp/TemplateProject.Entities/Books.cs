namespace TemplateProject.Entities;

public sealed class Book : IEntity
{
    public Guid Id { get; set; }
    public string Author { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int YearPublished { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
}
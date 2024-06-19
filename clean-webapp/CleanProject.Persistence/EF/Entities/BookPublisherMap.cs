namespace CleanProject.Persistence.EF.Entities;

public sealed class BookPublisherMap
{
    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
    public Guid PublisherId { get; set; }
    public Publisher Publisher { get; set; } = null!;
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
}
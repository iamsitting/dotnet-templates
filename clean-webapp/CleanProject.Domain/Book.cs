namespace CleanProject.Domain;

public class Book
{
    public string Title { get; set; } = null!;
    public int YearPublished { get; set; }
    public Guid Id { get; set; }

    public Author Author { get; set; } = null!;
    public List<Publisher> Publishers { get; set; } = [];
}
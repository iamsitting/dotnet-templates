namespace CleanProject.CoreApplication.Domain.Books;

public class UpdateBookDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
}
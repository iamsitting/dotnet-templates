using CleanProject.CoreApplication.Features.Books;

namespace CleanProject.Presentation.React.Areas.React;

public class BookViewModel
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public Guid AuthorId { get; set; }
    public int Year { get; set; }
    
    public BookViewModel(){}

    public AddBookCommand ToCreateCommand() => new(Title, AuthorId, Year, []);
    
}
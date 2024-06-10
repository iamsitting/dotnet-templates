using CleanProject.CoreApplication.Domain;

namespace CleanProject.CoreApplication.Features.Books;

public class BookDto : IBook, IWithKey<Guid>
{
    public string Author { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int YearPublished { get; set; }
    public Guid Id { get; set; }

    public BookDto(UpdateBookCommand command)
    {
        Id = command.Id;
        Author = command.Author;
        Title = command.Title;
        YearPublished = command.Year;
    }

    public BookDto(AddBookCommand command)
    {
        Author = command.Author;
        Title = command.Title;
        YearPublished = command.Year;
    }

    public BookDto(GetBookByIdQuery query)
    {
        Id = query.Id;
    }
    
    public BookDto(){}
}
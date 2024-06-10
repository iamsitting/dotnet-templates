using CleanProject.CoreApplication.Features.Books;

namespace CleanProject.Persistence.EF.Entities;

public sealed class Book : IEntity
{
    public Guid Id { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }
    public int YearPublished { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
    
    public BookDto AsDto()
    {
        return new BookDto(Id, Title, Author, YearPublished);
    }
    
    public void MapFromCommand(UpdateBookCommand command)
    {
        Title = command.Title;
        Author = command.Author;
        YearPublished = command.Year;
    }

    public Book(AddBookCommand command)
    {
        Title = command.Title;
        Author = command.Author;
        YearPublished = command.Year;
        CreatedOn = DateTime.Now;
    }
}
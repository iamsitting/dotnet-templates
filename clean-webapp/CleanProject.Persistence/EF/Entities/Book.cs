using CleanProject.CoreApplication.Domain;
using CleanProject.CoreApplication.Features.Books;

namespace CleanProject.Persistence.EF.Entities;

public sealed class Book : IBook, IBaseEntity, IWithKey<Guid>
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
        return new BookDto
        {
            Title = Title,
            Author = Author,
            YearPublished = YearPublished,
            Id = Id
        };
    }
    
    public void MapFromCommand(IBook command)
    {
        Title = command.Title;
        Author = command.Author;
        YearPublished = command.YearPublished;
    }

    public Book(IBook command)
    {
        Title = command.Title;
        Author = command.Author;
        YearPublished = command.YearPublished;
    }
}
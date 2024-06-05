using TemplateProject.Entities;

namespace TemplateProject.WebApi.React.Areas.React.Controllers;

public class BookViewModel
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    
    public BookViewModel(){}

    public BookViewModel(Book entity)
    {
        Id = entity.Id;
        Title = entity.Title;
        Author = entity.Author;
        Year = entity.YearPublished;
    }

    public Book ToEntity()
    {
        return new Book()
        {
            Id = new Guid(),
            Title = Title,
            Author = Author,
            YearPublished = Year
        };
    }

    public Book MapToEntity(Book entity)
    {
        entity.Title = Title;
        entity.Author = Author;
        entity.YearPublished = Year;
        return entity;
    }
}
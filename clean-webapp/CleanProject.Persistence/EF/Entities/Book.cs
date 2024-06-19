using CleanProject.CoreApplication.Features.Books;

namespace CleanProject.Persistence.EF.Entities;

public sealed class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public int YearPublished { get; set; }
    public bool IsArchived { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ArchivedOn { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    // reverse
    public List<BookPublisherMap> BookPublisherMaps { get; set; } = [];
    public Domain.Book AsDto()
    {
        return new Domain.Book
        {
            Title = Title,
            YearPublished = YearPublished,
            Id = Id,
            Author = ToDto(Author),
            Publishers = BookPublisherMaps.Select(x => ToDto(x.Publisher)).ToList()
        };
    }

    private static Domain.Author ToDto(Author entity)
    {
        return new Domain.Author()
        {
            Id = entity.Id,
            Name = entity.FullName()
        };
    }

    private static Domain.Publisher ToDto(Publisher entity)
    {
        return new Domain.Publisher()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    
    
    public void MapFromDto(Domain.Book command)
    {
        Id = command.Id;
        Title = command.Title;
        YearPublished = command.YearPublished;
        AuthorId = command.Author.Id;
    }

    public Book(Domain.Book command)
    {
        Title = command.Title;
        YearPublished = command.YearPublished;
        if (command.Author.Id == Guid.Empty)
        {
            Author = new Author()
            {
                FirstName = command.Author.Name,
                LastName = command.Author.Name,
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            };
        }
        else
        {
            AuthorId = command.Author.Id;
        }
        
    }
    public Book(){}
}
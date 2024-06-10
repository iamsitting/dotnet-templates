using CleanProject.CoreApplication.Domain;
using CleanProject.CoreApplication.Features.Books;

namespace CleanProject.Persistence.EF.Entities;

public sealed class Book : IBaseEntity, IWithKey<Guid>
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
    public BookDto AsDto()
    {
        return new BookDto
        {
            Title = Title,
            YearPublished = YearPublished,
            Id = Id,
            Author = ToDto(Author),
            Publishers = BookPublisherMaps.Select(x => ToDto(x.Publisher)).ToList()
        };
    }

    private static AuthorDto ToDto(Author entity)
    {
        return new AuthorDto()
        {
            Id = entity.Id,
            Name = entity.FullName()
        };
    }

    private static PublisherDto ToDto(Publisher entity)
    {
        return new PublisherDto()
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    
    
    public void MapFromDto(BookDto command)
    {
        Id = command.Id;
        Title = command.Title;
        YearPublished = command.YearPublished;
        AuthorId = command.Author.Id;
    }

    public Book(BookDto command)
    {
        Title = command.Title;
        YearPublished = command.YearPublished;
        AuthorId = command.Author.Id;
    }
    public Book(){}
}
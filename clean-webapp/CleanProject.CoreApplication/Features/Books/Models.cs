using CleanProject.CoreApplication.Domain;

namespace CleanProject.CoreApplication.Features.Books;

public class BookDto : IWithKey<Guid>
{
    public string Title { get; set; } = null!;
    public int YearPublished { get; set; }
    public Guid Id { get; set; }

    public AuthorDto Author { get; set; } = null!;
    public List<PublisherDto> Publishers { get; set; } = [];

    public BookDto(UpdateBookCommand command)
    {
        Id = command.Id;
        Title = command.Title;
        YearPublished = command.Year;
        Author = new AuthorDto() { Id = command.AuthorId };
    }

    public BookDto(AddBookCommand command)
    {
        Title = command.Title;
        YearPublished = command.Year;
        if (command.AuthorId != null)
        {
            Author = new AuthorDto() { Id = command.AuthorId.Value };    
        }
        else
        {
            Author = new AuthorDto()
            {
                Id = Guid.Empty,
                Name = command.AuthorName!,
            };
        }
        
        Publishers = command.PublisherIds.Select(x => new PublisherDto { Id = x }).ToList();
    }

    public BookDto(GetBookByIdQuery query)
    {
        Id = query.Id;
    }
    
    public BookDto(){}
}

public class AuthorDto : IWithKey<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class PublisherDto : IWithKey<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
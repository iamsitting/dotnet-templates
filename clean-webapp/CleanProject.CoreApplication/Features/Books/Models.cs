using CleanProject.Domain;

namespace CleanProject.CoreApplication.Features.Books;

public record AddBookCommand(string Title, int Year, Guid[] PublisherIds, Guid? AuthorId, string? AuthorName);

public record UpdateBookCommand(Guid Id, string Title, Guid AuthorId, int Year);

public record GetAllBooksQuery();

public record GetBookByIdQuery(Guid Id);

public static class BookMapper
{
    public static Book ToBook(this UpdateBookCommand command)
    {
        return new Book()
        {
            Id = command.Id,
            YearPublished = command.Year,
            Title = command.Title,
            Author = new Author()
            {
                Id = command.Id
            },
        };
    }

    public static Book ToBook(this AddBookCommand command)
    {
        return new Book()
        {
            Title = command.Title,
            YearPublished = command.Year,
            Author = new Author()
            {
                Id = command.AuthorId ?? Guid.Empty,
                Name = command.AuthorName ?? string.Empty
            },
            Publishers = command.PublisherIds.Select(x => new Publisher { Id = x }).ToList(),
        };
    }

    public static Book ToBook(this GetBookByIdQuery query)
    {
        return new Book()
        {
            Id = query.Id
        };
    }
}
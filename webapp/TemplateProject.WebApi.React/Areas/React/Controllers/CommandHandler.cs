using TemplateProject.Constants;
using TemplateProject.Database;
using TemplateProject.Entities;
using TemplateProject.Infrastructure.Caching;

namespace TemplateProject.WebApi.React.Areas.React.Controllers;

public class CommandHandler
{
    private readonly TemplateProjectContext _context;
    private readonly ICacheService _cache;

    public CommandHandler(TemplateProjectContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public BookViewModel Handle(CreateBookCommand command)
    {
        var book = new Book()
        {
            Title = command.Title,
            Author = command.Author,
            YearPublished = command.Year,
            CreatedOn = DateTime.Now
        };
        _context.Books.Add(book);
        _context.SaveChanges();
        _cache.ClearDomain(CacheKeys.Domain.Books);
        return new BookViewModel(book);
    }

    public BookViewModel Handle(UpdateBookCommand command)
    {
        var book = _context.Books.First(x => x.Id == command.Id);
        book.Title = command.Title;
        book.Author = command.Author;
        book.YearPublished = command.Year;
        _context.Books.Update(book);
        _context.SaveChanges();
        _cache.ClearDomain(CacheKeys.Domain.Books);
        return new BookViewModel(book);
    }
}
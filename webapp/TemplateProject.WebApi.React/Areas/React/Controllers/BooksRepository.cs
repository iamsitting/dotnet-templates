using TemplateProject.Constants;
using TemplateProject.Database;
using TemplateProject.Entities;
using TemplateProject.Infrastructure.Caching;

namespace TemplateProject.WebApi.React.Areas.React.Controllers;

public sealed class BooksRepository
{
    private readonly TemplateProjectContext _context;
    private readonly ICacheService _cache;

    public BooksRepository(TemplateProjectContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public IEnumerable<BookViewModel> GetBooks()
    {
        var key = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.All);
        var success = _cache.TryGet(key, out IEnumerable<Book> books);
        if (!success)
        {
            books = _context.Books.ToList();
            _cache.Set(key, books);
        }

        return books.Select(x => new BookViewModel(x));
    }

    public BookViewModel AddBook(BookViewModel book)
    {
        var entity = book.ToEntity();
        entity.CreatedOn = DateTime.Now;
        _context.Books.Add(entity);
        _context.SaveChanges();
        _cache.ClearDomain(CacheKeys.Domain.Books);
        return new BookViewModel(entity);
    }

    public BookViewModel UpdateBook(BookViewModel book)
    {
        var entity = _context.Books.First(x => x.Id == book.Id);
        entity = book.MapToEntity(entity);
        _context.Books.Update(entity);
        _context.SaveChanges();
        _cache.ClearDomain(CacheKeys.Domain.Books);
        return new BookViewModel(entity);
    }
}
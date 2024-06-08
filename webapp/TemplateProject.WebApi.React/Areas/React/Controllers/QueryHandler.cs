using TemplateProject.Constants;
using TemplateProject.Database;
using TemplateProject.Infrastructure.Caching;

namespace TemplateProject.WebApi.React.Areas.React.Controllers;

public class QueryHandler
{
    private readonly TemplateProjectContext _context;
    private readonly ICacheService _cache;

    public QueryHandler(TemplateProjectContext context)
    {
        _context = context;
    }

    public BookViewModel Handle(GetBookByIdQuery query)
    {
        var key = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.Id, query.Id.ToString());
        var success = _cache.TryGet(key, out BookViewModel book);
        if (success) return book;
        
        book = _context.Books.Select(x => new BookViewModel(x)).First(x => x.Id == query.Id);
        _cache.Set(key, book);
        return book;
    }

    public IEnumerable<BookViewModel> Handle(GetAllBooksQuery query)
    {
        var key = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.All);
        var success = _cache.TryGet(key, out IEnumerable<BookViewModel> books);
        if (success) return books;
        
        var q = _context.Books.AsQueryable();
        if (query.ExcludeArchive) q = q.Where(x => x.ArchivedOn == null);
        books = q.Select(x => new BookViewModel(x)).ToList();
        _cache.Set(key, books);
        return books;
    }
}
using CleanProject.CoreApplication.Constants;
using CleanProject.CoreApplication.Domain.Books;
using CleanProject.CoreApplication.Infrastructure.Caching;
using CleanProject.Persistence.EF;
using CleanProject.Persistence.Extensions;

namespace CleanProject.Persistence.Repositories;

public class BookRepository : IBookRepository
{
    private readonly CleanProjectContext _context;
    private readonly ICacheService _cache;

    public BookRepository(CleanProjectContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public IEnumerable<BookDto> GetAllBooks()
    {
        var key = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.All);
        var success = _cache.TryGet(key, out IEnumerable<BookDto> result);
        if (success) return result;

        result = _context.Books.Select(x => x.AsDto()).ToList();
        _cache.Set(key, result);
        return result;
    }

    public BookDto UpdateBook(UpdateBookDto data)
    {
        var entity = _context.Books.First(x => x.Id == data.Id);
        entity = data.MapToEntity(entity);
        _context.Books.Update(entity);
        _context.SaveChanges();
        _cache.ClearDomain(CacheKeys.Domain.Books);
        return entity.AsDto();
    }

    public BookDto GetBookById(Guid id)
    {
        var key = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.Id, id.ToString());
        var success = _cache.TryGet(key, out BookDto result);
        if (success) return result;

        result = _context.Books.Select(x => x.AsDto()).First(x => x.Id == id);
        _cache.Set(key, result);
        return result;
    }
}
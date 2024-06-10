using CleanProject.CoreApplication.Constants;
using CleanProject.CoreApplication.Features.Books;
using CleanProject.CoreApplication.Infrastructure.Caching;
using CleanProject.Persistence.EF;
using CleanProject.Persistence.EF.Entities;

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

    public IEnumerable<BookDto> Handle(GetAllBooksQuery _)
    {
        var key = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.All);
        var success = _cache.TryGet(key, out IEnumerable<BookDto> result);
        if (success) return result;

        result = _context.Books.Select(x => x.AsDto()).ToList();
        _cache.Set(key, result);
        return result;
    }

    public BookDto Handle(AddBookCommand command)
    {
        var entity = new Book(command);
        _context.Books.Add(entity);
        _cache.ClearDomain(CacheKeys.Domain.Books);
        return entity.AsDto();
    }

    public BookDto Handle(UpdateBookCommand command)
    {
        var entity = _context.Books.First(x => x.Id == command.Id);
        entity.MapFromCommand(command);
        _context.Books.Update(entity);
        _context.SaveChanges();
        _cache.ClearDomain(CacheKeys.Domain.Books);
        return entity.AsDto();
    }

    public BookDto Handle(GetBookByIdQuery query)
    {
        var key = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.Id, query.Id.ToString());
        var success = _cache.TryGet(key, out BookDto result);
        if (success) return result;

        result = _context.Books.Select(x => x.AsDto()).First(x => x.Id == query.Id);
        _cache.Set(key, result);
        return result;
    }
}
using CleanProject.CoreApplication.Constants;
using CleanProject.CoreApplication.Features.Books;
using CleanProject.CoreApplication.Infrastructure.Caching;
using CleanProject.Persistence.EF;
using CleanProject.Persistence.EF.Entities;
using Microsoft.EntityFrameworkCore;

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

    public IEnumerable<Domain.Book> GetAll()
    {
        var key = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.All);
        var success = _cache.TryGet(key, out IEnumerable<Domain.Book> result);
        if (success) return result;

        result = _context.Books
            .Include(x => x.Author)
            .Select(x => x.AsDto()).ToList();
        _cache.Set(key, result);
        return result;
    }

    public Domain.Book Add(Domain.Book command)
    {
        var entity = new Book(command)
        {
            Id = new Guid(),
            CreatedOn = DateTime.Now
        };
        _context.Books.Add(entity);
        _context.SaveChanges();
        var maps = command.Publishers.Select(x => new BookPublisherMap()
        {
            BookId = entity.Id,
            PublisherId = x.Id
        }).ToList();
        if (maps.Any())
        {
            _context.BookPublisherMaps.AddRange(maps);
            _context.SaveChanges();
        }
        _cache.ClearDomain(CacheKeys.Domain.Books);
        return entity.AsDto();
    }

    public Domain.Book Update(Domain.Book command)
    {
        var entity = _context.Books.First(x => x.Id == command.Id);
        entity.MapFromDto(command);
        _context.Books.Update(entity);
        _context.SaveChanges();
        _cache.ClearDomain(CacheKeys.Domain.Books);
        return entity.AsDto();
    }

    public Domain.Book GetById(Guid id)
    {
        var key = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.Id, id.ToString());
        var success = _cache.TryGet(key, out Domain.Book result);
        if (success) return result;

        result = _context.Books
            .Select(x => x.AsDto())
            .First(x => x.Id == id);
        _cache.Set(key, result);
        return result;
    }
}
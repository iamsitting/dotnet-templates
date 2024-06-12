using TemplateProject.Constants;
using TemplateProject.Database;
using TemplateProject.Entities;
using TemplateProject.Infrastructure.Caching;
using TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Books.Components;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Books;

internal class BookRepository : IBookRepository
{
    private readonly TemplateProjectContext _db;
    private readonly ICacheService _cache;

    public BookRepository(TemplateProjectContext db, ICacheService cache)
    {
        _db = db;
        _cache = cache;
    }

    private IEnumerable<Book> GetAllBooksFromCache()
    {
        var key = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.All);
        var success = _cache.TryGet(key, out List<Book> data);
        if (success) return data;

        data = _db.Books.ToList();
        _cache.Set(key, data);
        return data;
    }

    private Book GetBookByIdFromCache(Guid id)
    {
        var books = GetAllBooksFromCache();
        var book = books.First(x => x.Id == id);
        return book;
    }

    public IEnumerable<BookViewModel> GetBooks()
    {
        var books = GetAllBooksFromCache();
        return books.Select(x => new BookViewModel(x.Id, x.Title, x.Author, x.YearPublished));
    }

    public BookViewModel GetBookById(Guid id)
    {
        var book = GetBookByIdFromCache(id);
        return new BookViewModel(book.Id, book.Title, book.Author, book.YearPublished);
    }

    public BookViewModel UpdateBook(BookViewModel data)
    {
        var book = GetBookByIdFromCache(data.Id);
        book.Title = data.Title;
        book.Author = data.Author;
        book.YearPublished = data.Year;
        _db.Books.Update(book);
        _db.SaveChanges();
        var key = new CacheKey(CacheKeys.Domain.Books, CacheKeys.CacheType.All);
        _cache.Clear(key);
        return new BookViewModel(book.Id, book.Title, book.Author, book.YearPublished);
    }
}

public interface IBookRepository
{
    public BookViewModel UpdateBook(BookViewModel data);
    public BookViewModel GetBookById(Guid id);
    public IEnumerable<BookViewModel> GetBooks();
}
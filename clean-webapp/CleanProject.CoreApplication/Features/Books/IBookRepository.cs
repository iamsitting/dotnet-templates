using CleanProject.CoreApplication.Domain;

namespace CleanProject.CoreApplication.Features.Books;

public interface IBookRepository
{
    IEnumerable<BookDto> GetAll();
    BookDto Add(BookDto command);
    BookDto Update(BookDto command);
    BookDto GetById(IWithKey<Guid> query);
}
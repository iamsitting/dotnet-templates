using CleanProject.Domain;

namespace CleanProject.CoreApplication.Features.Books;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book Add(Book command);
    Book Update(Book command);
    Book GetById(Guid id);
}
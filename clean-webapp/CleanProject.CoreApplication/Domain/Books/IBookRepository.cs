namespace CleanProject.CoreApplication.Domain.Books;

public interface IBookRepository
{
    IEnumerable<BookDto> GetAllBooks();
    BookDto UpdateBook(UpdateBookDto data);
    BookDto GetBookById(Guid id);
}
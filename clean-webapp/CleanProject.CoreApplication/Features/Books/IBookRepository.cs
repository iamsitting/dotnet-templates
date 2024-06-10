namespace CleanProject.CoreApplication.Features.Books;

public interface IBookRepository
{
    IEnumerable<BookDto> Handle(GetAllBooksQuery _);
    BookDto Handle(GetBookByIdQuery query);
    BookDto Handle(AddBookCommand command);
    BookDto Handle(UpdateBookCommand command);
}
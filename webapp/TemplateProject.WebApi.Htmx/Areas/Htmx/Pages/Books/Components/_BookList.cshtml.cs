namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Books.Components;

public record BookViewModel(Guid Id, string Title, string Author, int Year);
public record BookList(IEnumerable<BookViewModel> Books);

public class BookListActions
{
    private readonly IBookRepository _repository;

    public BookListActions(IBookRepository repository)
    {
        _repository = repository;
    }

    public ComponentResult BookList()
    {
        var books = _repository.GetBooks();
        return new ComponentResult("Components/_BookList", new BookList(books));
    }
}
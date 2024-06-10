namespace CleanProject.CoreApplication.Features.Books;

public record GetAllBooksQuery();
public record GetBookByIdQuery(Guid Id);

public class QueryHandler
{
    private readonly IBookRepository _repository;

    public QueryHandler(IBookRepository repository)
    {
        _repository = repository;
    }


    public BookDto Handle(GetBookByIdQuery query)
    {
        var book = _repository.Handle(query);
        return book;
    }

    public IEnumerable<BookDto> Handle(GetAllBooksQuery query)
    {
        var books = _repository.Handle(query);
        return books;
    }
}
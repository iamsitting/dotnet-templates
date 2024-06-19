using CleanProject.CoreApplication.Infrastructure;
using CleanProject.Domain;

namespace CleanProject.CoreApplication.Features.Books;

public class BookService
{
    private readonly IBookRepository _repository;
    private readonly IAppLogger<BookService> _logger;

    public BookService(IBookRepository repository, IAppLogger<BookService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public Book Handle(GetBookByIdQuery query)
    {
        var book = _repository.GetById(query.Id);
        return book;
    }
    public async Task HandleAsync(AddBookCommand command)
    {
        if (command.Year > DateTime.Now.Year)
        {
            var ex = new Exception("Invalid");
            _logger.LogError("Failed to add book", ex);
            throw ex;
        }

        if (string.IsNullOrEmpty(command.Title))
        {
            var ex = new Exception("Invalid");
            _logger.LogError("Failed to add book", ex);
            throw ex;
        }
        
        _repository.Add(command.ToBook());
        
        _logger.LogInformation("Success!");
    }

    public async Task<IEnumerable<Book>> HandleAsync(GetAllBooksQuery _)
    {
        var books = _repository.GetAll();
        return books;
    }
}
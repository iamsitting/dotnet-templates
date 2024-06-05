using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.WebApi.React.Areas.React.Controllers;

public class BooksController : AreaControllerBase
{
    private readonly BooksRepository _repository;

    public BooksController(BooksRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var results = _repository.GetBooks();
        return Ok(results);
    }
}
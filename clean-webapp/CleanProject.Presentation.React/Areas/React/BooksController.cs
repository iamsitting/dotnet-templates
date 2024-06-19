using CleanProject.CoreApplication.Features.Books;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Presentation.React.Areas.React;

public class BooksController : AreaControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var results = await _bookService.HandleAsync(new GetAllBooksQuery());
        return Ok(results);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] BookViewModel payload)
    {
        await _bookService.HandleAsync(payload.ToCreateCommand());
        return Ok();
    }
}
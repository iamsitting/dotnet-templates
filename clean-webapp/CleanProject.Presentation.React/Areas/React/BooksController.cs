using CleanProject.CoreApplication.Features.Books;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Presentation.React.Areas.React;

public class BooksController : AreaControllerBase
{
    private readonly CommandHandler _commandHandler;
    private readonly QueryHandler _queryHandler;

    public BooksController(CommandHandler commandHandler, QueryHandler queryHandler)
    {
        _commandHandler = commandHandler;
        _queryHandler = queryHandler;
    }


    [HttpGet]
    public IActionResult Index()
    {
        var results = _queryHandler.Handle(new GetAllBooksQuery());
        return Ok(results);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] BookViewModel payload)
    {
        await _commandHandler.HandleAsync(payload.ToCreateCommand());
        return Ok();
    }
}
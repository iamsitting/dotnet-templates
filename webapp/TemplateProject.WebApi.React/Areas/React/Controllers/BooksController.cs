using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.WebApi.React.Areas.React.Controllers;

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
    public IActionResult Add([FromBody] BookViewModel payload)
    {
        var result = _commandHandler.Handle(payload.ToCreateCommand());
        return Ok(result);
    }
}
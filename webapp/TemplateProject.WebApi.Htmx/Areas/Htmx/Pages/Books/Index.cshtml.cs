using Microsoft.AspNetCore.Mvc;
using TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Books.Components;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Books;

public class Index : HtmxPageModel
{
    private readonly BookListActions _bookListActions;
    private readonly EditRowActions _editRowActions;

    public Index(IBookRepository repository)
    {
        _bookListActions = new BookListActions(repository);
        _editRowActions = new EditRowActions(repository);
    }

    public IActionResult OnGet()
    {
        PageTitle = "Books";        
        return BoostedPage();
    }

    public IActionResult OnGetBookList()
    {
        var component = _bookListActions.BookList();
        return Partial(component.View, component.Model);
    }

    public IActionResult OnGetEditRow([FromQuery] Guid id)
    {
        var component = _editRowActions.EditRow(id);
        return Partial(component.View, component.Model);
    }

    public IActionResult OnPutSubmitRow([FromForm] BookViewModel payload)
    {
        var component = _editRowActions.SubmitRow(payload);
        return Partial(component.View, component.Model);
    }
}
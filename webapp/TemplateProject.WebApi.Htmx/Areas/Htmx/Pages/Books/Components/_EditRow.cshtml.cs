using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Books.Components;

public record EditRowModel(BookViewModel Book, bool IsEdit = false);

public record ComponentResult(string View, object? Model = null);

public class EditRowActions
{
    private readonly IBookRepository _repository;
    public EditRowActions(IBookRepository repository)
    {
        _repository = repository;
    }

    public ComponentResult EditRow(Guid id)
    {
        var book = _repository
            .GetBookById(id);
        return new ComponentResult("Components/_EditRow", new EditRowModel(book, true));
    }

    public ComponentResult SubmitRow(BookViewModel payload)
    {
        var book = _repository
            .UpdateBook(payload);
        return new ComponentResult("Components/_EditRow", new EditRowModel(book));
    }
}
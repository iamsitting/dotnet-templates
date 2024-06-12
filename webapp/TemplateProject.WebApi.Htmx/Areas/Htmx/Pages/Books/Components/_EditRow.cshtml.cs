using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Books.Components;

public record EditRowModel(BookViewModel Book, bool IsEdit = false);

public static class EditRowHandlers
{
    public static IActionResult EditRow(this Index page, Guid id)
    {
        var book = page.DbContext.Books.First(x => x.Id == id);
        var vm = new BookViewModel(book.Id, book.Title, book.Author, book.YearPublished);
        return page.Partial("Components/_EditRow", new EditRowModel(vm, true));
    }
}

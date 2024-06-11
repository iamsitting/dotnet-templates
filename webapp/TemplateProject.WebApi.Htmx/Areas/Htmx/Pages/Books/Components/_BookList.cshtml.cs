using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Books.Components;

public record BookViewModel(string Title, string Author, int Year);
public record BookList(IEnumerable<BookViewModel> Books);

public static class BookListHandlers
{
    public static IActionResult BookList(this Index page)
    {
        var items = page.DbContext.Books.ToList().Select(x => new BookViewModel(x.Title, x.Author, x.YearPublished));
        return page.Partial("Components/_BookList", new BookList(items));
    }
}
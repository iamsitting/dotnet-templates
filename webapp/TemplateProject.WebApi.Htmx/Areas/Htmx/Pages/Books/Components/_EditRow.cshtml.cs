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

    public static IActionResult SubmitRow(this Index page, BookViewModel payload)
    {
        var book = page.DbContext.Books.First(x => x.Id == payload.Id);
        book.Title = payload.Title;
        book.Author = payload.Author;
        book.YearPublished = payload.Year;
        page.DbContext.Books.Update(book);
        page.DbContext.SaveChanges();
        return page.Partial("Components/_EditRow", new EditRowModel(payload));
    }
}

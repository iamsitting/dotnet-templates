using Microsoft.AspNetCore.Mvc;
using TemplateProject.Database;
using TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Books.Components;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Books;

public class Index : HtmxPageModel
{
    public readonly TemplateProjectContext DbContext;

    public Index(TemplateProjectContext context)
    {
        DbContext = context;
    }

    public IActionResult OnGet()
    {
        PageTitle = "Books";        
        return BoostedPage();
    }

    public IActionResult OnGetBookList()
    {
        return this.BookList();
    }

    public IActionResult OnGetEditRow([FromQuery] Guid id)
    {
        return this.EditRow(id);
    }
}
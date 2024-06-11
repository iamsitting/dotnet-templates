using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Books;

public class Index : HtmxPageModel
{
    public IActionResult OnGet()
    {
        return BoostedPage();
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages;

public class Index : PageModel
{
    public void OnGet()
    {
        
    }

    public IActionResult OnGetLoginForm()
    {
        return Partial("Landing/_LoginForm");
    }

    public IActionResult OnGetRegisterForm()
    {
        return Partial("Landing/_RegisterForm");
    }
}
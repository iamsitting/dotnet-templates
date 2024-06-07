using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Entities.Identity;


namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages;

public class Index : HtmxPageModel
{
    private readonly SignInManager<AppUser> _signInManager;

    public Index(SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public void OnGet()
    {
        
    }

    public IActionResult OnGetLoginForm()
    {
        return Partial("Landing/_LoginForm");
    }

    public async Task<IActionResult> OnPostLoginFormAsync([FromForm] LoginFormPayload payload)
    {
        Console.WriteLine(string.Join(",", HttpContext.Request.Form.Select(x => x.Key)));
        var result = await _signInManager.PasswordSignInAsync(payload.Email, payload.Password, false, true);
        if (!result.Succeeded)
        {
            return ForbiddenContent("Incorrect e-mail or password");
        }
        return HtmxPage();
    }

    public IActionResult OnGetRegisterForm()
    {
        return Partial("Landing/_RegisterForm");
    }
}

public record LoginFormPayload(string Email, string Password);
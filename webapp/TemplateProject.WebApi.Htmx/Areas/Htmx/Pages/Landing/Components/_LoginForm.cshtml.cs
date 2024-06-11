using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Landing.Components;

public record LoginFormPayload(string Email, string Password);

public static class LoginFormHandlers
{
    public static IActionResult LoginForm(this Index page, object? model=null)
    {
        return page.Partial("Components/_LoginForm", model);
    }
    
    public static async Task<IActionResult> LoginFormAsync(this Index page, LoginFormPayload payload)
    {
        var result = await page.SignInManager.PasswordSignInAsync(payload.Email, payload.Password, false, true);
        if (!result.Succeeded)
        {
            return page.ForbiddenContent("Incorrect e-mail or password");
        }
        return page.BoostedPage();
    }
}
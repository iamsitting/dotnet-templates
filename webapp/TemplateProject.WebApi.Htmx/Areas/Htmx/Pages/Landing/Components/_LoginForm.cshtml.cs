using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Landing.Components;

public record LoginFormPayload(string Email, string Password);

public static class LoginFormHandlers
{
    public static IActionResult LoginForm(this Index page, object? model=null)
    {
        page.Response.Headers["HX-Push-Url"] = "#login";
        return page.Partial("Components/_LoginForm", model);
    }
    
    public static async Task<IActionResult> LoginFormAsync(this Index page, LoginFormPayload payload)
    {
        var user = await page.Repository.GetUserByEmailAsync(payload.Email);
        var success = await page.Repository.SignIn(user, payload.Password);
        if (!success) return page.ForbiddenContent("Sign in attempt failed");
        return page.BoostedPage();
    }
}
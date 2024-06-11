using Microsoft.AspNetCore.Mvc;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Landing.Components;

public record RegisterFormPayload(string Email, string Password, string ConfirmPassword);

public static class RegisterFormHandlers
{
    public static IActionResult RegisterForm(this Index page, object? model = null)
    {
        page.Response.Headers["HX-Push-Url"] = "#register";
        return page.Partial("Components/_RegisterForm", model);
    }

    public static async Task<IActionResult> RegisterFormAsync(this Index page, RegisterFormPayload payload)
    {
        if (!string.Equals(payload.Password, payload.ConfirmPassword))
            return page.ErrorContent("Passwords don't match");
        var user = await page.Repository.CreateUserAsync(payload.Email, payload.Password);
        
        await page.Repository.SignIn(user);
        return page.BoostedPage();
    }
}
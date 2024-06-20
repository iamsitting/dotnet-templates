using CleanProject.CoreApplication.Features.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Presentation.Hypermedia.Areas.Hypermedia.Pages.Landing.Components;
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
        var result = await page._AuthService.HandleAsync(new CreateUserCommand(payload.Email, payload.Password));
        page.Response.Cookies.Append("hx.token", result.Token);
        return page.BoostedPage();
    }
}
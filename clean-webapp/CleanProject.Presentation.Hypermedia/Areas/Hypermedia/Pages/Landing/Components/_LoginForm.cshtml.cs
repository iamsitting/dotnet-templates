using CleanProject.CoreApplication.Features.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Presentation.Hypermedia.Areas.Hypermedia.Pages.Landing.Components;

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
        var token = await page._AuthService.Handle(new GetUserQuery(payload.Email, payload.Password));
        page.Response.Cookies.Append("hx.token", token.Token);
        return page.BoostedPage();
    }
}
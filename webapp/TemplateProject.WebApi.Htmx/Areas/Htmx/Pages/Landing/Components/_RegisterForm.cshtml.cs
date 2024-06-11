using Microsoft.AspNetCore.Mvc;
using TemplateProject.Entities.Identity;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Landing.Components;

public record RegisterFormPayload(string Email, string Password);

public static class RegisterFormHandlers
{
    public static IActionResult RegisterForm(this Index page, object? model = null)
    {
        return page.Partial("Components/_RegisterForm", model);
    }

    public static async Task<IActionResult> RegisterFormAsync(this Index page, RegisterFormPayload payload)
    {
        var user = new AppUser() { UserName = payload.Email, Email = payload.Email };
        var result = await page.UserManager.CreateAsync(user, payload.Password);
        if (!result.Succeeded) return page.BadRequest(result.Errors);

        await page.SignInManager.SignInAsync(user, true);
        return page.BoostedPage();
    }
}
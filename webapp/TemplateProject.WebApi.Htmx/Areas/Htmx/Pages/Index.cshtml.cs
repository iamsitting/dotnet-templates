using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Entities.Identity;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages;

public class Index : HtmxPageModel
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
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
        var result = await _signInManager.PasswordSignInAsync(payload.Email, payload.Password, false, true);
        if (!result.Succeeded)
        {
            return ForbiddenContent("Incorrect e-mail or password");
        }
        return HtmxPage();
    }

    public IActionResult OnGetRegisterForm([FromForm] RegisterFormPayload payload)
    {
        return Partial("Landing/_RegisterForm");
    }

    public async Task<IActionResult> OnPostRegisterFormAsync([FromForm] RegisterFormPayload payload)
    {
        var user = new AppUser() { UserName = payload.Email, Email = payload.Email };
        var result = await _userManager.CreateAsync(user, payload.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        await _signInManager.SignInAsync(user, true);
        return HtmxPage();
    }
}

public record LoginFormPayload(string Email, string Password);

public record RegisterFormPayload(string Email, string Password);
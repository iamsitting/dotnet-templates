using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Entities.Identity;
using TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Landing.Components;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Landing;

public class Index : HtmxPageModel
{
    public readonly SignInManager<AppUser> SignInManager;
    public readonly UserManager<AppUser> UserManager;
    public Index(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        SignInManager = signInManager;
        UserManager = userManager;
    }

    public void OnGet()
    {
        
    }

    public IActionResult OnGetLoginForm()
    {
        return this.LoginForm();
    }

    public async Task<IActionResult> OnPostLoginFormAsync([FromForm] LoginFormPayload payload)
    {
        return await this.LoginFormAsync(payload);
    }

    public IActionResult OnGetRegisterForm()
    {
        return this.RegisterForm();
    }

    public async Task<IActionResult> OnPostRegisterFormAsync([FromForm] RegisterFormPayload payload)
    {
        return await this.RegisterFormAsync(payload);
    }
}
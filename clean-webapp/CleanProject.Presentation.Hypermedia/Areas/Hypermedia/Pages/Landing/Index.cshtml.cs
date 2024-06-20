
using CleanProject.CoreApplication.Features.Auth;
using CleanProject.Persistence.Repositories;
using CleanProject.Presentation.Hypermedia.Areas.Hypermedia.Pages.Landing.Components;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Presentation.Hypermedia.Areas.Hypermedia.Pages.Landing;

public class Index : HxPageModel
{
    public readonly AuthService _AuthService;

    public Index(AuthService authService)
    {
        _AuthService = authService;
    }


    public IActionResult OnGet()
    {
        return BoostedPage();
    }

    public IActionResult OnGetLoginForm()
    {
        return this.LoginForm();
    }

    public async Task<IActionResult> OnPostLoginFormAsync([FromForm] LoginFormPayload payload)
    {
        return await this.LoginFormAsync(payload);
    }

    // public IActionResult OnGetRegisterForm()
    // {
    //     return this.RegisterForm();
    // }
    //
    // public async Task<IActionResult> OnPostRegisterFormAsync([FromForm] RegisterFormPayload payload)
    // {
    //     return await this.RegisterFormAsync(payload);
    // }
}

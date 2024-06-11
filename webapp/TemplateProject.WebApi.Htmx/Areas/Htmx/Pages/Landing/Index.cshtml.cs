using Microsoft.AspNetCore.Mvc;
using TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Landing.Components;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Landing;

public class Index : HtmxPageModel
{
    public readonly LandingRepository Repository;
    public Index(LandingRepository repository)
    {
        Repository = repository;
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

    public IActionResult OnGetRegisterForm()
    {
        return this.RegisterForm();
    }

    public async Task<IActionResult> OnPostRegisterFormAsync([FromForm] RegisterFormPayload payload)
    {
        return await this.RegisterFormAsync(payload);
    }
}
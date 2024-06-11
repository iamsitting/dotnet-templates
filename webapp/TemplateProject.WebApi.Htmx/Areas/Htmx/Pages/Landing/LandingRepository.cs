using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TemplateProject.Entities.Identity;

namespace TemplateProject.WebApi.Htmx.Areas.Htmx.Pages.Landing;

public class LandingRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ILogger<LandingRepository> _logger;

    public LandingRepository(UserManager<AppUser> userManager, ILogger<LandingRepository> logger, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _logger = logger;
        _signInManager = signInManager;
    }

    public async Task<AppUser> GetUserByEmailAsync(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);
        if (result == null) throw new Exception("Could not find a user with that e-mail");
        return result;
    }

    public async Task<AppUser> CreateUserAsync(string email, string password)
    {
        var user = new AppUser
        {
            UserName = email,
            Email = email
        };
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to create user");
        }

        return user;
    }

    public async Task<bool> SignIn(AppUser user, string? password = null)
    {
        if (string.IsNullOrEmpty(password))
        {
            await _signInManager.SignInAsync(user, false);
            return true;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        return result.Succeeded;
    }
    
}
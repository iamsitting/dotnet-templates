using CleanProject.CoreApplication.Features.Auth;
using CleanProject.Domain;
using CleanProject.Persistence.EF.Entities.Identity;
using CleanProject.Persistence.Extension;
using Microsoft.AspNetCore.Identity;
using User = CleanProject.Persistence.EF.Entities.Identity.User;

namespace CleanProject.Persistence.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<User> _userManager;

    public AuthRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AppUser?> CreateUserAsync(string username, string email, string password)
    {
        var user = new User()
        {
            UserName = username,
            Email = email
        };
        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded ? user.ToUser() : null;
    }

    public async Task<AppUser?> GetUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return null;
        var success = await _userManager.CheckPasswordAsync(user, password);
        return success ? user.ToUser() : null;
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Entities.Identity;
using TemplateProject.Infrastructure.Tokens;
using TemplateProject.WebApi.Extensions;

namespace TemplateProject.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    // POST: api/account/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] string email, [FromBody] string password)
    {
        var user = new AppUser() { UserName = email, Email = email };
        var result = await _userManager.CreateAsync(user, password);
        
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            var token = _tokenService.GenerateJwtTokenForClaims(user.GetJwtClaims());
            return Ok(new { Token = token });
        }

        return BadRequest(result.Errors);
    }

    // POST: api/account/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] string email, [FromBody] string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = _tokenService.GenerateJwtTokenForClaims(user!.GetJwtClaims());
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }

    // POST: api/account/logout
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out successfully.");
    }

    
}
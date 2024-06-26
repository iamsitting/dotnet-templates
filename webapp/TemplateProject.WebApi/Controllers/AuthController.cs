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

    public record RegisterPayload(string Email, string Password);
    // POST: api/account/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterPayload payload)
    {
        var user = new AppUser() { UserName = payload.Email, Email = payload.Email };
        var result = await _userManager.CreateAsync(user, payload.Password);
        
        if (result.Succeeded)
        {
            var token = _tokenService.GenerateJwtTokenForClaims(user.GetJwtClaims());
            return Ok(new { Token = token });
        }

        return BadRequest(result.Errors);
    }
    
    // POST: api/account/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] RegisterPayload payload)
    {
        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user == null) return Unauthorized("Could not find an associated account");

        var success = await _userManager.CheckPasswordAsync(user, payload.Password);

        if (!success) return Unauthorized();

        var token = _tokenService.GenerateJwtTokenForClaims(user.GetJwtClaims());
        return Ok(new { Token = token });

    }

    // POST: api/account/logout
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out successfully.");
    }

    
}
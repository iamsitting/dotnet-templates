using CleanProject.CoreApplication.Infrastructure.Token;
using CleanProject.Persistence.EF.Entities.Identity;
using CleanProject.Presentation.React.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Presentation.React.Areas.React;

public class AuthController : AreaControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public record RegisterPayload(string Email, string Password);
    // POST: /api/_react/auth/register
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
    
    // POST: api/_react/auth/login
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
}
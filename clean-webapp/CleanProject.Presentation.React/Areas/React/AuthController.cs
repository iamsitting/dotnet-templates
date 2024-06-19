using CleanProject.CoreApplication.Features.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.Presentation.React.Areas.React;

public class AuthController : AreaControllerBase
{
    
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    public record RegisterPayload(string Email, string Password);
    // POST: /api/_react/auth/register
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterPayload payload)
    {
        var result = await _service.HandleAsync(new CreateUserCommand(payload.Email, payload.Password));
        return Ok(new { result.Token });
    }
    
    // POST: api/_react/auth/login
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] RegisterPayload payload)
    {
        var resp = await _service.Handle(new GetUserQuery(payload.Email, payload.Password));
        return Ok(new { Token = resp.Token });

    }
}